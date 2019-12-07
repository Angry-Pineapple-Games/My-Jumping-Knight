using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSpawner : MonoBehaviour
{
    public Image TutorialText;
    public string keyString;
    Language lang;
    // Start is called before the first frame update
    void Start()
    {
        lang = GameObject.Find("Language(Clone)").GetComponent<Language>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTutorial()
    {
        TutorialText.GetComponentInChildren<Text>().text = lang.GetString(keyString);
    }
}
