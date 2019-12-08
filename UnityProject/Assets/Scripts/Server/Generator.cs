using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject apiClient;
    public GameObject language;
    /*Genera los gameobject que permanecerán en todas las escenas*/
    void Awake()
    {
        if (GameObject.Find("ApiClient(Clone)") == null)
            Instantiate(apiClient);
        if (GameObject.Find("Language(Clone)") == null)
            Instantiate(language);
    }
}
