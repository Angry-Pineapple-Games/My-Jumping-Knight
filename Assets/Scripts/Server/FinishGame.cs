using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    /*Solicita la recogida de información a ManagerAPI*/
    void Start()
    {
        ManagerAPI apiClient = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>();
        StartCoroutine(apiClient.FinishGame());
    }
}
