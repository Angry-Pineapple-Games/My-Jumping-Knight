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
        language = PlayerPrefs.GetString("lang", "en");
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
        if (language.Equals("es")) { return Spanish[key]; }
        else if (language.Equals("en")) { return English[key]; }
        else { return null; }
    }

    public void InitDictionaries()
    {
        #region English
        #region Access
        English.Add("requirement", "Numbers and letters without accents only,\nno blank spaces for the username");
        English.Add("username", "Username");
        English.Add("password", "Password");
        English.Add("passwordcheck", "Confirm password");
        English.Add("login", "Login");
        English.Add("signup", "Sign up");
        English.Add("back", "Go back");
        #endregion

        #region Menu
        English.Add("settings", "Settings");
        English.Add("credits", "Credits");
        English.Add("play", "Play");
        English.Add("level1", "Level 1");
        English.Add("level2", "Level 2");
        English.Add("level3", "Level 3");
        English.Add("mode", "Mode\nselection");
        English.Add("singleplayer", "Single player");
        English.Add("multiplayer", "Multiplayer");
        English.Add("menu", "Menu");
        English.Add("manual", "Manual play");
        English.Add("auto", "Autoplay");
        English.Add("loading", "Loading");
        English.Add("userexist", "User already exist");
        English.Add("badcredentials", "Credentials error!");
        English.Add("soundsoff", "Sounds off");
        English.Add("soundson", "Sounds on");
        English.Add("musicoff", "Music off");
        English.Add("musicon", "Music on");
        English.Add("license", "License");
        English.Add("attribution", "Licensed By Creative Commons with Attribution 3.0");
        English.Add("team", "Team members");
        #endregion

        #region Game
        English.Add("again", "Play again");
        English.Add("rank", "RANK:");
        English.Add("victory", "VICTORY");
        #endregion

        #region General
        English.Add("maintenance", "Download this game\nto play online!");
        English.Add("ok", "Ok");
        #endregion

        #region Tutorial
        English.Add("tutMovMobile", "Touch one of the spheres surounding the knight to move him the way you want");
        English.Add("tutMovPC", "Use WASD/directional arrows or click on the spheres with the mouse to move the knight the way you want");
        English.Add("tutMovAlert", "Pressing very fast in one direction is not appropiate. Try to follow the knight's pace");
        English.Add("tutSpikes", "Spikes get triggered when you walk over them. Be fast!");
        English.Add("tutSaw", "The saw always goes from one side to the other. Try to avoid it");
        English.Add("tutArrows", "Wait for the best moment to avoid the arrows");
        English.Add("tutBlade", "Watch out for the razor blade! It's really fast!");
        English.Add("tutHeart", "Get a heart to recover one life");
        English.Add("tutShield", "Grab the shield to prevent the next damage you would get");
        English.Add("tutClock", "The hourglass makes traps go slower for a while. Take your chance!");
        English.Add("tutFall", "If you move until the end of a path, you will fall. Be careful!");
        English.Add("tutGoal", "Reach the goal to finish the level");
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
        Spanish.Add("mode", "Selección\nde modo");
        Spanish.Add("singleplayer", "Individual");
        Spanish.Add("multiplayer", "Multijugador");
        Spanish.Add("menu", "Menú");
        Spanish.Add("manual", "Partida manual");
        Spanish.Add("auto", "Partida automática");
        Spanish.Add("loading", "Cargando");
        Spanish.Add("userexist", "El usuario ya existe");
        Spanish.Add("badcredentials", "Error de credenciales!");
        Spanish.Add("soundsoff", "Desactivar sonidos");
        Spanish.Add("soundson", "Activar sonidos");
        Spanish.Add("musicoff", "Desactivar música");
        Spanish.Add("musicon", "Activar música");
        Spanish.Add("license", "Licencias");
        Spanish.Add("attribution", "Licencia Creative Commons con Atribución 3.0");
        Spanish.Add("team", "Miembros del equipo");
        #endregion

        #region Game
        Spanish.Add("again", "Jugar de nuevo");
        Spanish.Add("rank", "RANGO:");
        Spanish.Add("victory", "VICTORIA");
        #endregion

        #region General
        Spanish.Add("maintenance", "Descarga el juego\npara jugar online!");
        Spanish.Add("ok", "Ok");
        #endregion

        #region Tutorial
        Spanish.Add("tutMovMobile", "Pulsa sobre las esferas alrededor del caballero para moverlo en la dirección que escojas");
        Spanish.Add("tutMovPC", "Utiliza WASD, las teclas direccionales o pulsa con el ratón las esferas para mover al caballero en la dirección que escojas");
        Spanish.Add("tutMovAlert", "Pulsar muy rápido en una dirección no es lo apropiado, intenta seguir el ritmo del caballero");
        Spanish.Add("tutSpikes", "Los pinchos se accionan cuando pasas por encima de ellos, ¡pasa rápido!");
        Spanish.Add("tutSaw", "La sierra siempre se mueve de un lado a otro, intenta evitarla");
        Spanish.Add("tutArrows", "Espera al momento oportuno y podrás esquivar las flechas");
        Spanish.Add("tutBlade", "¡Cuidado con la cuchilla giratoria! ¡Es muy rápida!");
        Spanish.Add("tutHeart", "Obtén un corazón para recuperar una vida");
        Spanish.Add("tutShield", "Coge el escudo para prevenir el próximo daño");
        Spanish.Add("tutClock", "El reloj de arena hará que todas las trampas funcionen más despacio durante un tiempo, ¡aprovecha tu oportunidad!");
        Spanish.Add("tutFall", "Si te pasas con el movimiento, caerás al vacío, ¡ve con cuidado!");
        Spanish.Add("tutGoal", "Llega a la meta para finalizar el nivel");
        #endregion
        #endregion
    }
}
