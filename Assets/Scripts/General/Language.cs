using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    private Dictionary<string, string> Spanish = new Dictionary<string, string>();
    private Dictionary<string, string> English = new Dictionary<string, string>();
    [HideInInspector]
    public string language;
    void Start()
    {
        DontDestroyOnLoad(this);
        language = PlayerPrefs.GetString("lang","en");
        InitDictionaries();
    }

    public void LanguageSelector(string lang)
    {
        if (lang.Equals("es") || lang.ToLower().Equals("spanish"))
            language = "es";
        else if (lang.Equals("en") || lang.ToLower().Equals("english"))
            language = "en";
        else
            Debug.LogError("Fail error in Language => LanguageSelectoer => parameter lang");
    }

    public string GetString(string key)
    {
        if(language.Equals("es")) { return Spanish[key]; }
        else if(language.Equals("en")) { return English[key]; }
        else { return null; }
    }

    public void InitDictionaries()
    {
        #region English
        #region Access
        English.Add("requirement", "Only numbers and letters without accents,\nno spaces in username case");
        English.Add("username", "Username");
        English.Add("password", "Password");
        English.Add("passwordcheck", "Confirm password");
        English.Add("login", "Login");
        English.Add("signup", "Sign up");
        English.Add("back", "Back");
        #endregion

        #region Menu
        English.Add("settings", "Settings");
        English.Add("credits", "Credits");
        English.Add("play", "Play");
        English.Add("level1", "Level 1");
        English.Add("level2", "Level 2");
        English.Add("level3", "Level 3");
        English.Add("mode", "Mode selection");
        English.Add("singleplayer", "Singleplayer");
        English.Add("multiplayer", "Multiplayer");
        English.Add("menu", "Menu");
        English.Add("manual", "Manual-play");
        English.Add("auto", "Auto-play");
        #endregion

        #region Game
        English.Add("again", "Play again");
        #endregion

        #region General
        English.Add("maintenance", "Server in maintenance,\nsorry for the inconvenience");
        English.Add("ok", "Ok");
        #endregion
        #endregion

        #region Spanish
        #region Access
        Spanish.Add("requirement", "Sólo números y letras sin tildes,\nsin espacios en el caso del nombre de usuario");
        Spanish.Add("username", "Usuario");
        Spanish.Add("password", "Contraseña");
        Spanish.Add("passwordcheck", "Confirmar contraseña");
        Spanish.Add("login", "Iniciar sesión");
        Spanish.Add("signup", "Registrarse");
        Spanish.Add("back", "Volver");
        #endregion

        #region Menu
        Spanish.Add("settings", "Opciones");
        Spanish.Add("credits", "Créditos");
        Spanish.Add("play", "Jugar");
        Spanish.Add("level1", "Nivel 1");
        Spanish.Add("level2", "Nivel 2");
        Spanish.Add("level3", "Nivel 3");
        Spanish.Add("mode", "Selección de modo");
        Spanish.Add("singleplayer", "Un jugador");
        Spanish.Add("multiplayer", "Multijugador");
        Spanish.Add("menu", "Menú");
        Spanish.Add("manual", "Partida manual");
        Spanish.Add("auto", "Partida automática");
        #endregion

        #region Game
        Spanish.Add("again", "Jugar de nuevo");
        #endregion

        #region General
        Spanish.Add("maintenance", "Servidor en mantenimiento\nsentimos las molestias");
        Spanish.Add("ok", "Ok");
        #endregion
        #endregion
    }
}
