using UnityEngine;
using UnityEngine.SceneManagement;

/*Proporciona un script que puede ser aderido a cualquier objeto, con el fin
 de posibilitar un cambio de escena*/
public class ChangeScene : MonoBehaviour
{
    private const string MULTIPLAYERKEY = "MultiplayerGame";
    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MultiplayerButton(string sceneName)
    {
        PlayerPrefs.SetInt(MULTIPLAYERKEY, 1);
        ChangeToScene(sceneName);
    }

    public void SingleplayerButton(string sceneName)
    {
        PlayerPrefs.SetInt(MULTIPLAYERKEY, 0);
        ChangeToScene(sceneName);
    }
}
