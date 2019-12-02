using UnityEngine;

public class FinishedGame : MonoBehaviour
{
    private ManagerAPI apiClient;
    void Start()
    {
        apiClient = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>();
        print("Partida terminada");
        if (apiClient.isNetReachability) { StartCoroutine(apiClient.Record("nuevo", "C", "1")); }
    }
}
