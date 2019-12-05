using UnityEngine;

public class APIGenerator : MonoBehaviour
{
    public GameObject apiClient;

    /*Genera un único apiclient el cual permanecerá en todas las escenas, gestionando así las peticiones que sean necesarias*/
    void Start()
    {
        if (GameObject.Find("ApiClient(Clone)") == null)
            Instantiate(apiClient);
    }
}
