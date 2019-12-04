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
    private bool debug = true;
    private const int NUMBERLEVELS = 3;
    private const int NUMBERGAMES = 20;
    private const string GAME = "MainScene";
    private string[] RANKS = new string[] { "C", "B", "A", "A+", "S", "S+" };
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
        if (mode.Equals("Login")) { Login(msg.msg, username, password); }
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

    public IEnumerator Record(string levelstring, string rank, string level)
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

    public IEnumerator FinishGame()
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
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            myUsername = "Anonimous";
            isNetReachability = false;
            SceneManager.LoadScene(GAME);
        }
        else { isNetReachability = true; }
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
    private void UpdateLevelUserPlayerPrefs(string level, string levelstring)
    {
        string key = myUsername + "level" + level;
        string[] newLevelString = levelstring.Split(null);
        string[] oldLevelString = PlayerPrefs.GetString(key).Split(' ');
        if (newLevelString.Length == 1 || (int.Parse(newLevelString[newLevelString.Length - 1]) >= int.Parse(oldLevelString[newLevelString.Length - 1]) 
            && float.Parse(newLevelString[newLevelString.Length - 2]) < float.Parse(oldLevelString[newLevelString.Length - 2])))
            PlayerPrefs.SetString(key, levelstring);
    }

    /*Devuelve una partida guardada al azar*/
    public string[] GetRandomOponent()
    {
        string[] oponentString = "".Split(' ');
        oponentUsername = oponentString[0];
        string[] result = new string[oponentString.Length-2];
        Array.Copy(oponentString, 1, result, 0, oponentString.Length - 2);
        return result;
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