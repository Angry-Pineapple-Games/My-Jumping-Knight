using UnityEngine.UI;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour
{
    #region Variables
    private bool debug = true;
    public InputField username;
    public InputField password;
    public InputField passwordCheck;

    private const int SIZEUSERNAME = 30;
    private const int SIZEPASSWORD = 120;
    #endregion

    /*Métodos que proporcionan los comportamientos necesarios para controlar los envios al servidor
     de forma asíncrona*/
    public void SendRegistration()
    {
        bool usernameCorrect = !username.text.Equals("") && username.text.Length <= SIZEUSERNAME && CheckIsLetterNumberOnly(username.text);
        bool passwordCorrect = !password.text.Equals("") && password.text.Length <= SIZEPASSWORD && CheckIsLetterNumberOnly(password.text, true);
        bool passwordsEquals = password.text.Equals(passwordCheck.text);

        if (passwordCorrect && usernameCorrect && passwordsEquals)
        {
            ManagerAPI apiClient = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>();

            StartCoroutine(apiClient.PostRequest(username.text, password.text, "Registration"));
            if (debug) { Debug.Log("Registro enviado"); }
        }
        else
        {
            if (debug)
            {
                Debug.Log("Ni el nombre de usuario ni la contraseña pueden estar vacios y las contraseñas deben coincidir," +
       " máximo de caracteres para el usuario 30 y 120 para la contraseña");
            }
        }
    }

    public void SendLogin()
    {
        bool usernameCorrect = !username.text.Equals("") && username.text.Length <= SIZEUSERNAME && CheckIsLetterNumberOnly(username.text);
        bool passwordCorrect = !password.text.Equals("") && password.text.Length <= SIZEPASSWORD && CheckIsLetterNumberOnly(password.text, true);

        if (passwordCorrect && usernameCorrect)
        {
            ManagerAPI apiClient = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>();

            StartCoroutine(apiClient.PostRequest(username.text, password.text, "Login"));
            if (debug) { Debug.Log("Login enviado"); }
        }
        else
        {
            if (debug) { Debug.Log("Ni el nombre de usuario ni la contraseña pueden estar vacios y las contraseñas deben coincidir"); }
        }
    }

    /*Comprueba que el string dado esté formado sólo por letras y números
     space = true; espacios permitidos
     space = false; espacios no permitidos*/
    private bool CheckIsLetterNumberOnly(string str, bool space = false)
    {
        foreach (char c in str)
        {
            if (!char.IsLetter(c) && !char.IsNumber(c))
                if (space && !c.Equals(" "))
                    return false;
                else if (!space)
                    return false;
        }
        return true;
    }
}
