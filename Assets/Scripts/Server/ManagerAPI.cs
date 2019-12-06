using System.Collections;
using UnityEngine;
using System;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/*Gestor de las peticiones necesarias del juego al servidor*/
class ManagerAPI : MonoBehaviour
{
    #region Variables
    //objetos
    public GameObject serverMaintenanceWarning;

    //generales
    private bool debug = true;
    private const int NUMBERLEVELS = 3;
    private const int NUMBERGAMES = 20;
    private const float MAXTIMETOSAVE = 1000f;
    private const string GAME = "MainScene";
    private string[] RANKS = new string[] { "C", "B", "A", "A+", "S", "S+" };
    private int[] RANKSSTEPS = new int[] { 120, 95, 55, 30, 10, 2};
    [HideInInspector]
    public bool isNetReachability;

    //seguridad
    private RSACryptoServiceProvider myprivateKey;
    private string myModulus;
    private string myExponent;
    private RSACryptoServiceProvider serverKey;
    private string myAccessToken;
    private string myRefreshToken;
    //usuario
    [HideInInspector]
    public string myUsername;
    [HideInInspector]
    public string myPassword;
    [HideInInspector]
    public float myGlobalTime;
    [HideInInspector]
    public string myRank1 = "";
    [HideInInspector]
    public string myLevel1 = "";
    [HideInInspector]
    public string myRank2 = "";
    [HideInInspector]
    public string myLevel2 = "";
    [HideInInspector]
    public string myRank3 = "";
    [HideInInspector]
    public string myLevel3 = "";
    //oponente
    [HideInInspector]
    public string oponentUsername;
    [HideInInspector]
    public string oponentGlobalTime;
    //partida
    private string[] sampleGames;
    #endregion

    public void Start()
    {
        DontDestroyOnLoad(this);

        /*Genera los parametros necesarios para que el servidor genere la llave publica
         del cliente, además de generar la llave publica del servidor para poder encriptar
         los mensajes que enviaremos a este*/
        myprivateKey = A_encrypter.RSAgenerateKey();
        RSAParameters parameters = myprivateKey.ExportParameters(false);
        myModulus = Convert.ToBase64String(parameters.Modulus);
        myExponent = Convert.ToBase64String(parameters.Exponent);
        serverKey = A_encrypter.RSApublicConstruct();

        /*Asegura al cliente que haya partidas guardadas y comprueba
         la disponibilidad de internet*/
        FirstExecution();
    }

    #region APIComunication
    public IEnumerator PostRequest(string username, string password, string mode)
    {
        string json = JsonUtility.ToJson(new PostRequest(username, password, myModulus, myExponent));
        WWWForm form = new WWWForm();
        form.AddField("msg", A_encrypter.RSAencrypt(json, serverKey));

        string url;
        if (mode.Equals("Login")) { url = "http://localhost:5000/login"; }
        else if (mode.Equals("Registration")) { url = "http://localhost:5000/registration"; }
        else { url = ""; }
        UnityWebRequest client = UnityWebRequest.Post(url, form);

        yield return client.SendWebRequest();

        Msg msg = JsonUtility.FromJson<Msg>(client.downloadHandler.text);
        if (msg == null) { ServerMaintenance(); }
        else if (mode.Equals("Login")) { Login(msg.msg, username, password); }
        else if (mode.Equals("Registration")) { Registration(msg.msg, username, password); }
    }

    private void Login(string msg, string username, string password)
    {
        AnswerLogin aLogin = JsonUtility.FromJson<AnswerLogin>(A_encrypter.RSAdecrypt(msg, myprivateKey));

        if (aLogin.error == null)
        {
            myAccessToken = aLogin.access_token;
            myUsername = username;
            myPassword = password;
            myRank1 = aLogin.myrank1;
            myLevel1 = aLogin.mylevel1;
            myRank2 = aLogin.myrank2;
            myLevel2 = aLogin.mylevel2;
            myRank3 = aLogin.myrank3;
            myLevel3 = aLogin.mylevel3;
            if (debug)
            {
                Debug.Log("Login successful");
                Debug.Log("Username: " + username);
                Debug.Log("Password: " + password);
                Debug.Log("AccessToken: " + aLogin.access_token);
                Debug.Log("Level1: " + aLogin.level1);
                Debug.Log("Level2: " + aLogin.level2);
                Debug.Log("Level3: " + aLogin.level3);
                Debug.Log("myRank1: " + aLogin.myrank1);
                Debug.Log("myLevel1: " + aLogin.mylevel1);
                Debug.Log("myRank2: " + aLogin.myrank2);
                Debug.Log("myLevel2: " + aLogin.mylevel2);
                Debug.Log("myRank3: " + aLogin.myrank3);
                Debug.Log("myLevel3: " + aLogin.mylevel3);
            }

            // almacenamiento en playerprefs
            /*Si no existe crea los playerprefs con informacion de un dummy para el usuario
             Se actualizan los datos locales con los del servidor si son partidas mejores*/
            InitUserPlayerPrefs();
            UpdateLevelUserPlayerPrefs("1", myLevel1);
            UpdateLevelUserPlayerPrefs("2", myLevel3);
            UpdateLevelUserPlayerPrefs("3", myLevel3);

            SceneManager.LoadScene(GAME);
        }
        else
        {
            if (debug) { Debug.Log(aLogin.error); }
        }
    }

    private void Registration(string msg, string username, string password)
    {
        AnswerRegistration aRegistration = JsonUtility.FromJson<AnswerRegistration>(A_encrypter.RSAdecrypt(msg, myprivateKey));
        if (aRegistration.error == null)
        {
            myAccessToken = aRegistration.access_token;
            myRefreshToken = aRegistration.refresh_token;
            myUsername = username;
            myPassword = password;
            if (debug)
            {
                Debug.Log("Registration receive");
                Debug.Log("Username: " + username);
                Debug.Log("Password: " + password);
                Debug.Log("AccessToken: " + aRegistration.access_token);
                Debug.Log("RefreshToken: " + aRegistration.refresh_token);
            }
            InitUserPlayerPrefs();
            SceneManager.LoadScene(GAME);
        }
        else
        {
            if (debug) { Debug.Log(aRegistration.error); }
        }
    }

    private void ServerMaintenance()
    {
        Instantiate(serverMaintenanceWarning, GameObject.Find("Canvas").transform);
    }

    public IEnumerator Record(string levelstring, string rank, string level)
    {
        if (isNetReachability)
        {
            string json = JsonUtility.ToJson(new RecordRequest(myUsername, myPassword, levelstring, rank, level, myModulus, myExponent));
            WWWForm form = new WWWForm();
            form.AddField("msg", A_encrypter.RSAencrypt(json, serverKey));

            UnityWebRequest client = UnityWebRequest.Post("http://localhost:5000/update", form);
            client.SetRequestHeader("Authorization", "Bearer " + myAccessToken);

            yield return client.SendWebRequest();

            Msg apiresp = JsonUtility.FromJson<Msg>(client.downloadHandler.text);
            if (apiresp.error != null) { Debug.LogError(apiresp.error); }
            if (debug) { Debug.Log(apiresp.msg); }
        }
    }

    public IEnumerator FinishGame()
    {
        if (isNetReachability)
        {
            UnityWebRequest client = UnityWebRequest.Get("http://localhost:5000/games");
            client.SetRequestHeader("Authorization", "Bearer " + myAccessToken);

            yield return client.SendWebRequest();

            Msg msg = JsonUtility.FromJson<Msg>(client.downloadHandler.text);
            AnswerFinishGame aFinishGame = JsonUtility.FromJson<AnswerFinishGame>(msg.msg);
            if (debug)
            {
                Debug.Log(aFinishGame.level1);
                Debug.Log(aFinishGame.level2);
                Debug.Log(aFinishGame.level3);
            }
            UpdateLoopPlayerPrefs("1", aFinishGame.level1);
            UpdateLoopPlayerPrefs("2", aFinishGame.level2);
            UpdateLoopPlayerPrefs("3", aFinishGame.level3);
        }
    }
    #endregion

    #region ClientStorage
    /*Si es la primera ejecución genera los archivos de almacenamiento en el cliente
     y guarda las partidas por defecto, si no, recoje las que tenga guardadas el cliente
     por si la conexión no se puede establecer*/
    private void FirstExecution()
    {
        GamesString eString = new GamesString();
        if (!PlayerPrefs.HasKey("level1C_1")) // primera partida en esta máquina
        {
            if (debug) { Debug.Log("First execution"); }
            LoopPlayerPrefs("Set", eString);
        }
        LoopPlayerPrefs("Get", eString);

        //si no hay conexión a internet salta al menú del juego
        if (Application.internetReachability == NetworkReachability.NotReachable) { NetUnavailable(); }
        else { isNetReachability = true; }
    }

    /*Si la red o el servidor no están disponibles, crea un perfil anónimo para la partida*/
    public void NetUnavailable()
    {
        myUsername = "Anonimous";
        myRank1 = "C";
        myLevel1 = "";
        myRank2 = "C";
        myLevel2 = "";
        myRank3 = "C";
        myLevel3 = "";
        isNetReachability = false;
        SceneManager.LoadScene(GAME);
    }

    /*Recorre el almacenamiento en el cliente modificando los playerprefs
     * formados por "level" + level + rank + "_" + num
     Set: crear para la primera ejecución del juego en una maquina
     Get: recoger*/
    private void LoopPlayerPrefs(string mode, GamesString es)
    {
        string[] levelComplete = null;
        for (int level = 1; level <= NUMBERLEVELS; level++)// por cada nivel
        {
            string strlevel = "level" + level.ToString();
            foreach (string rank in RANKS) //por cada rango
            {
                levelComplete = es.GetLevel(level, rank); //coge el nivel predefinido
                string strlevelrank = strlevel + rank;
                for (int num = 1; num <= levelComplete.Length; num++) //son 20 ejemplos
                {
                    string levelxr_num = strlevelrank + "_" + num.ToString();
                    if (mode.Equals("Set"))
                        PlayerPrefs.SetString(levelxr_num, levelComplete[num - 1]);
                    else if (mode.Equals("Get"))
                    {
                        levelComplete[num - 1] = PlayerPrefs.GetString(levelxr_num);
                    }
                }
            }
        }
        if (mode.Equals("Get")) // guarda el nuevo paquete de partidas
            sampleGames = levelComplete;
    }

    /*con el paquete del servidor, lo desgrana y guarda para disponer de ellas en local,
     guardándolas en los correspondientes playerprefs
     key = "level" + level + rank + "_" + num*/
    private void UpdateLoopPlayerPrefs(string level, string levelString)
    {
        string[] games = levelString.Split(';');
        if (games.Length < NUMBERGAMES) { Debug.LogError("Fail ManagerAPI => UpdateLoopPlayerPrefs => Not enough examples"); }
        else
        {
            foreach (string rank in RANKS)
            {
                for (int i = 1; i <= NUMBERGAMES; i++)
                {
                    string key = "level" + level + rank + "_" + i.ToString();
                    PlayerPrefs.SetString(key, games[i - 1]);
                }
            }
        }
    }

    /*Para la primera ejecución en esta máquina guadarda en las partidas del usuario,
     los ejemplos guardados en el cliente
     key = username + "mylevel" + num*/
    private void InitUserPlayerPrefs()
    {
        for (int i = 1; i <= NUMBERLEVELS; i++)
        {
            string key = myUsername + "level" + i.ToString();
            if (!PlayerPrefs.HasKey(key))
            {
                string dummy = PlayerPrefs.GetString("level" + i.ToString() + "C_1");
                PlayerPrefs.SetString(key, dummy);
            }
            else
            {
                break;
            }
        }
    }

    /*Guarda en local la mejor partida del cliente (compara la pasada con la existente)
     si es nuevo lo guarda o 
     si tiene más o la misma vida y si ha tardado menos tiempo*/
    public void UpdateLevelUserPlayerPrefs(string level, string levelstring, bool saveRecord = false, float min = 0f, int heal = 0, float globaltime = 0f)
    {
        string keylevel = myUsername + "level" + level;
        string keyrank = myUsername + "rank" + level;
        string[] newLevelString = levelstring.Split(null);
        string[] oldLevelString = PlayerPrefs.GetString(keylevel).Split(' ');
        if (oldLevelString.Length == 1 || (int.Parse(newLevelString[newLevelString.Length - 1]) >= int.Parse(oldLevelString[oldLevelString.Length - 1])
            && float.Parse(newLevelString[newLevelString.Length - 2]) < float.Parse(oldLevelString[oldLevelString.Length - 2])))
        {
            if (saveRecord && globaltime < MAXTIMETOSAVE)
            {
                string rank = CalculateRank(min, heal, globaltime, level);
                Record(levelstring, rank, level);
                PlayerPrefs.SetString(keyrank, rank);
            }
            PlayerPrefs.SetString(keylevel, levelstring);
        }
    }

    /*Devuelve una partida guardada al azar*/
    public string[] GetRandomOponent()
    {
        string[] oponentString = "Anonimous 0.5470238 0 0.3064338 0 0.2650674 1 0.285378 1 0.2551659 0 0.712975 0 0.1990519 0 0.8186679 0 0.2348395 0 0.6957635 0 0.2152901 0 0.9012651 0 0.1990334 0 0.3901187 2 0.2317388 2 0.3643652 3 0.2319336 3 0.2319335 0 0.1828229 0 0.2154013 0 0.215447 0 0.2003622 0 0.4346209 1 0.2153089 1 0.2170456 0 0.2039373 0 0.1654085 0 0.1831699 0 0.1818203 2 0.2012313 2 0.1659252 2 0.1428727 2 0.4425962 2 0.6792347 0 0.2003937 0 0.7308339 0 0.1916556 0 0.7615975 0 0.2174611 0 0.1818039 0 0.1670991 1 0.2177493 1 0.1695576 1 0.1822291 1 0.2323987 1 0.2125824 0 0.2051836 0 0.1868592 0 0.1725153 0 0.1822322 0 0.1988001 0 1.115306 0 0.1826388 1 0.2493269 0 0.1985533 0 0.1657264 2 0.215024 0 0.2822436 0 0.2147561 2 0.2708285 2 0.2318145 0 0.8117852 0 0.2046163 0 0.8181848 0 0.1835046 0 0.1983762 1 0.199426 1 0.2147408 0 0.2319326 0 0.1912805 0 0.1988014 0 0.1827008 0 0.1817662 0 0.1697755 0 0.1823934 0 0.1993301 0 0.1748024 0 0.2452022 2 0.1825396 2 0.1820706 2 0.234366 0 0.6686519 0 0.1661983 0 0.1965239 0 0.665051 0 0.2078766 1 0.1994013 1 0.1822882 3 0.2279387 3 0.4780333 3 0.2551482 1 0.2148106 1 0.1656662 0 0.5404963 3 0.2319293 1 0.2153666 1 0.1826415 0 0.2152067 1 0.2350504 0 0.2156068 0 0.2157637 0 0.215362 2 0.7763159 2 0.2213826 0 0.1658567 2 0.2194689 2 0.2235991 2 0.1825015 0 0.2178989 0 0.2160799 0 0.3809826 2 0.1710127 2 0.2822207 0 0.7121455 0 0.2507453 1 0.1987994 1 0.2488443 0 0.5554714 3 0.2543689 1 0.2186186 1 0.2364374 1 0.2347626 1 0.2001201 0 1.140078 0 0.2032231 0 0.3532527 0 0.2365176 2 0.3172708 3 1.423941 2 0.314768 3 1.349639 0 0.1989627 2 0.2059783 2 0.1491007 0 0.248501 0 0.3313332 0 0.2486308 0 0.2187991 1 0.1988008 1 0.2323161 0 0.2165359 0 1.105016 0 0.1906291 1 0.2981966 0 0.1656665 1 0.7274005 2 0.2317892 0 0.236661 0 0.2616932 2 0.2153655 0 0.2371374 0 0.2153656 0 0.1859791 0 0.1895266 0 0.2029108 0 0.1912845 0 0.447301 0 0.5142823 2 0.1980836 2 0.1995938 2 0.1980062 2 0.1992328 2 0.2663063 3 0.1990213 3 0.3149075 0 0.2316196 0 0.679818 1 0.1841584 1 0.1658068 1 0.6776404 1 0.3290581 0 0.8290619 0 0.1987999 0 0.1993201 0 0.1986934 0 1.28296 0 0.2578345 0 0.1989264 0 0.2189813 0 0.1988017 0 0.1988609 0 0.1987394 0 0.1861259 0 0.1998854 0 0.1980908 0 0.2981424 0 0.2660293 1 58.09721 0".Split(' ');
        oponentUsername = oponentString[0];
        oponentGlobalTime = oponentString[oponentString.Length - 2];
        string[] result = new string[oponentString.Length - 2];
        Array.Copy(oponentString, 1, result, 0, oponentString.Length - 2);
        return result;
    }

    /*Calcula el rango obtenido por el jugador en la partida*/
    private string CalculateRank(float min, int heal, float globaltime, string level)
    {
        string result;
        if (heal <= 0){ result = RANKS[0];}
        else if(heal == 1)
        {
            if (globaltime > min + RANKSSTEPS[1]) { result = RANKS[0]; }
            else { result = RANKS[1]; }
        }
        else if (heal == 2) {
            if(globaltime > min + RANKSSTEPS[1]) { result = RANKS[0]; }
            else if(globaltime > min + RANKSSTEPS[2]) { result = RANKS[1]; }
            else if(globaltime > min + RANKSSTEPS[3]) { result = RANKS[2]; }
            else { result = RANKS[3]; }
        }
        else
        {
            if (globaltime > min + RANKSSTEPS[1]) { result = RANKS[0]; }
            else if (globaltime > min + RANKSSTEPS[2]) { result = RANKS[1]; }
            else if (globaltime > min + RANKSSTEPS[3]) { result = RANKS[2]; }
            else if (globaltime > min + RANKSSTEPS[4]) { result = RANKS[3]; }
            else if (globaltime > min + RANKSSTEPS[5]) { result = RANKS[4]; }
            else { result = RANKS[5]; }
        }

        if(level.Equals("1")) { return myRank1 = result;}
        else if(level.Equals("2")) { return myRank2 = result;}
        else if(level.Equals("3")) { return myRank3 = result;}
        else {
            Debug.LogError("Fail ManagerAPI => CalculateRank => level wrong");
            return null;
        }

    }
    #endregion
}

#region ObjectsToSend
/*Objetos que serán enviados al servidor en el registro y el login*/
public class PostRequest
{
    public string username;
    public string password;
    public string modulus;
    public string exponent;

    public PostRequest(string name, string pass, string mod, string exp)
    {
        this.username = name;
        this.password = pass;
        this.modulus = mod;
        this.exponent = exp;
    }
}

public class RecordRequest
{
    public string username;
    public string password;
    public string levelstring;
    public string rank;
    public string level;
    public string modulus;
    public string exponent;

    public RecordRequest(string name, string pass, string levelstring,
        string rank, string level, string mod, string exp)
    {
        this.username = name;
        this.password = pass;
        this.levelstring = levelstring;
        this.rank = rank;
        this.level = level;
        this.modulus = mod;
        this.exponent = exp;
    }
}
#endregion

#region ObjectsToReceive
/*Contenedor del mensaje encriptado para el servidor API*/
public class Msg
{
    public const string MSGKEY = "msg";
    public string msg;
    public string error;

    public Msg(string msg)
    {
        this.msg = msg;
    }
}

/*Objetos recibidos del servidor*/
public class AnswerRegistration
{
    public string access_token;
    public string refresh_token;
    public string error;
}

public class AnswerLogin
{
    public string access_token;
    public string myrank1;
    public string mylevel1;
    public string myrank2;
    public string mylevel2;
    public string myrank3;
    public string mylevel3;
    public string level1;
    public string level2;
    public string level3;
    public string error;
}

public class AnswerFinishGame
{
    public string accessToken;
    public string level1;
    public string level2;
    public string level3;
    public string error;
}
#endregion