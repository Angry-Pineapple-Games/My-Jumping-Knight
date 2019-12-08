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
    public GameObject iconLoading;
    public GameObject userWarning;
    public GameObject credentialsWarning;

    //generales
    private bool debug = false;
    private const int NUMBERLEVELS = 3;
    private const int NUMBERGAMES = 5;
    private const float MAXTIMETOSAVE = 1000f;
    private const string ADDRESS = "http://angrygame.ddns.net:5000/";
    private const string GAME = "MainScene";
    private const string LOGIN = "Login";
    private string[] RANKS = new string[] { "C", "B", "A", "A+", "S", "S+" };
    private int[] RANKSSTEPS1 = new int[] { 120, 57, 37, 22, 10, 2};
    private int[] RANKSSTEPS2 = new int[] { 120, 100, 60, 35, 15, 5};
    private int[] RANKSSTEPS3 = new int[] { 120, 100, 65, 45, 25, 15};
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
    public IEnumerator PostRequest(string username, string password, string mode, ButtonBehaviours button)
    {
        string json = JsonUtility.ToJson(new PostRequest(username, password, myModulus, myExponent));
        WWWForm form = new WWWForm();
        form.AddField("msg", A_encrypter.RSAencrypt(json, serverKey));

        string url;
        if (mode.Equals("Login")) { url = ADDRESS + "login"; }
        else if (mode.Equals("Registration")) { url = ADDRESS + "registration"; }
        else { url = ""; }
        UnityWebRequest client = UnityWebRequest.Post(url, form);

        yield return client.SendWebRequest();

        Msg msg = JsonUtility.FromJson<Msg>(client.downloadHandler.text);
        if (msg == null) { ServerMaintenance(); }
        else if (mode.Equals("Login")) { Login(msg.msg, username, password); }
        else if (mode.Equals("Registration")) { Registration(msg.msg, username, password); }
        button.pressed = false;
        Destroy(button.icon);
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
            UpdateUserPlayerPrefs("1", myLevel1, myRank1);
            UpdateUserPlayerPrefs("2", myLevel2, myRank2);
            UpdateUserPlayerPrefs("3", myLevel3, myRank3);

            SceneManager.LoadScene(GAME);
        }
        else
        {
            if (debug) { Debug.Log(aLogin.error); }
            if (aLogin.error.Equals("Bad credentials"))
                CredentialsWarning();
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
            SceneManager.LoadScene(LOGIN);
        }
        else
        {
            if (debug) { Debug.Log(aRegistration.error); }
            if (aRegistration.error.Equals("User already exist"))
                UserWarning();
        }
    }

    private void ServerMaintenance()
    {
        Instantiate(serverMaintenanceWarning, GameObject.Find("Canvas").transform);
        isNetReachability = false;
    }

    private void UserWarning()
    {
        Instantiate(userWarning, GameObject.Find("Canvas").transform);
    }

    private void CredentialsWarning()
    {
        Instantiate(credentialsWarning, GameObject.Find("Canvas").transform);
    }

    public IEnumerator Record(string levelstring, string rank, string level)
    {
        if (isNetReachability)
        {
            string json = JsonUtility.ToJson(new RecordRequest(myUsername, myPassword, levelstring, rank, level, myModulus, myExponent));
            WWWForm form = new WWWForm();
            form.AddField("msg", A_encrypter.RSAencrypt(json, serverKey));

            UnityWebRequest client = UnityWebRequest.Post(ADDRESS + "update", form);
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
            UnityWebRequest client = UnityWebRequest.Get(ADDRESS + "games");
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
        if (!PlayerPrefs.HasKey("lvl1C_1")) // primera partida en esta máquina
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
        InitUserPlayerPrefs();
        UpdateUserPlayerPrefs("1", myLevel1, myRank1);
        UpdateUserPlayerPrefs("2", myLevel2, myRank2);
        UpdateUserPlayerPrefs("3", myLevel3, myRank3);
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
            string strlevel = "lvl" + level.ToString();
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
                    string key = "lvl" + level + rank + "_" + i.ToString();
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
                string dummy = PlayerPrefs.GetString("lvl" + i.ToString() + "C_1");
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
    public void UpdateUserPlayerPrefs(string level, string newlevel, string rank)
    {
        string keylevel = myUsername + "level" + level;
        string keyrank = myUsername + "rank" + level;
        string[] newLevelString = newlevel.Split(null);
        string oldlevel = PlayerPrefs.GetString(keylevel);
        string[] oldLevelString = oldlevel.Split(' ');
        string oldrank = PlayerPrefs.GetString(keyrank);
        if(newLevelString.Length == 1)
        {
            string sample = PlayerPrefs.GetString("lvl" + level + "C_1");
            PlayerPrefs.SetString(keylevel, sample);
            PlayerPrefs.SetString(keyrank, "C");
        }
        else if(int.Parse(newLevelString[newLevelString.Length - 1]) >= int.Parse(oldLevelString[oldLevelString.Length - 1])
            && float.Parse(newLevelString[newLevelString.Length - 2]) < float.Parse(oldLevelString[oldLevelString.Length - 2]))
        {
            PlayerPrefs.SetString(keylevel, newlevel);
            PlayerPrefs.SetString(keyrank, rank);
        }
        else
        {
            PlayerPrefs.SetString(keylevel, oldlevel);
            PlayerPrefs.SetString(keyrank, oldrank);
        }
    }


    /*Guarda la partida si es la mejor que tiene el cliente tanto los movimientos
     como el rango*/
    public void SaveRecordLevelUser(string level, string levelstring, float min, int heal, float globaltime)
    {
        string keylevel = myUsername + "level" + level;
        string keyrank = myUsername + "rank" + level;
        string[] newLevelString = levelstring.Split(null);
        string[] oldLevelString = PlayerPrefs.GetString(keylevel).Split(' ');
        string rank = CalculateRank(min, heal, globaltime, level);
        PlayerPrefs.SetString("lastRank", rank);
        int oldHeal = int.Parse(oldLevelString[oldLevelString.Length - 1]);
        int newHeal = int.Parse(newLevelString[newLevelString.Length - 1]);
        float oldTime = float.Parse(newLevelString[newLevelString.Length - 2]);
        float newTime = float.Parse(oldLevelString[oldLevelString.Length - 2]);
        if (!oldLevelString[0].Equals(myUsername) || oldHeal == 0 || (newHeal >= oldHeal && oldTime < newTime))
        {
            if (globaltime < MAXTIMETOSAVE)
            {
                StartCoroutine(Record(levelstring, rank, level));
                PlayerPrefs.SetString(keyrank, rank);
                PlayerPrefs.SetString(keylevel, levelstring);
            }
        }
    }

    /*Devuelve una partida guardada al azar*/
    public string[] GetRandomOponent(string level)
    {
        string rank;
        if (myRank1.Equals(""))
            rank = "C";
        else
            rank = myRank1;
        string key = "lvl" + level + rank + "_" + UnityEngine.Random.Range(1, 5).ToString();
        string[] oponentString = PlayerPrefs.GetString(key).Split(' ');
        oponentUsername = oponentString[0];
        oponentGlobalTime = oponentString[oponentString.Length - 2];
        string[] result = new string[oponentString.Length - 2];
        Array.Copy(oponentString, 1, result, 0, oponentString.Length - 2);
        return result;
    }

    public string[] GetBestPlayerLevel(string level)
    {
        string[] levelString = PlayerPrefs.GetString(myUsername + "level" + level).Split(' ');
        string[] result = new string[levelString.Length - 2];
        Array.Copy(levelString, 1, result, 0, levelString.Length - 2);
        return result;
    }

    /*Calcula el rango obtenido por el jugador en la partida*/
    private string CalculateRank(float min, int heal, float globaltime, string level)
    {
        string result;
        int[] ranksSteps;
        if(level == "1")
        {
            ranksSteps = RANKSSTEPS1;
        }else if(level == "2")
        {
            ranksSteps = RANKSSTEPS2;
        }
        else
        {
            ranksSteps = RANKSSTEPS3;
        }
        if (heal <= 0){ result = RANKS[0];}
        else if(heal == 1)
        {
            if (globaltime > min + ranksSteps[1]) { result = RANKS[0]; }
            else { result = RANKS[1]; }
        }
        else if (heal == 2) {
            if(globaltime > min + ranksSteps[1]) { result = RANKS[0]; }
            else if(globaltime > min + ranksSteps[2]) { result = RANKS[1]; }
            else if(globaltime > min + ranksSteps[3]) { result = RANKS[2]; }
            else { result = RANKS[3]; }
        }
        else
        {
            if (globaltime > min + ranksSteps[1]) { result = RANKS[0]; }
            else if (globaltime > min + ranksSteps[2]) { result = RANKS[1]; }
            else if (globaltime > min + ranksSteps[3]) { result = RANKS[2]; }
            else if (globaltime > min + ranksSteps[4]) { result = RANKS[3]; }
            else if (globaltime > min + ranksSteps[5]) { result = RANKS[4]; }
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