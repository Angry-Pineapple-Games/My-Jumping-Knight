using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LangWriter : MonoBehaviour
{
    private Language lang;
    private string lastLang;
    private Text textfield;
    private string key;
    /*Cambiar el campo text por el valor correspondiente del script Language
     teniendo que estar junto un Text(Script) que tendra el valor clave*/
    void Start()
    {
        textfield = GetComponent<Text>();
        lang = GameObject.Find("Language(Clone)").GetComponent<Language>();
        lastLang = lang.language;
        key = textfield.text;
        textfield.text = lang.GetString(textfield.text);
        if (SceneManager.GetActiveScene().name.Equals("Login"))
            StartCoroutine(CheckLangChange());
    }

    IEnumerator CheckLangChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => !lang.language.Equals(lastLang));
            lastLang = lang.language;
            textfield.text = lang.GetString(key);
        }
    }
}
