using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Escribe el nombre de los usuarios en un campo de texto*/
public class Username : MonoBehaviour
{
    private Text textfield;
    void Start()
    {
        Text textField = GetComponent<Text>();
        if (textField.text.Equals("username"))
            textField.text = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>().myUsername;
        else if (textField.text.Equals("oponent"))
            textField.text = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>().oponentUsername;
    }
}
