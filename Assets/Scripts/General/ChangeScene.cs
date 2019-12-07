using UnityEngine;
using UnityEngine.SceneManagement;

/*Proporciona un script que puede ser aderido a cualquier objeto, con el fin
 de posibilitar un cambio de escena*/
public class ChangeScene : MonoBehaviour
{
    private const string MULTIPLAYERKEY = "MultiplayerGame";
    private const string AUTOPLAYKEY = "AutoplayGame";
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

    public void AutoplayButton(string sceneName)
    {
        PlayerPrefs.SetInt(AUTOPLAYKEY, 1);
        ChangeToScene(sceneName);
    }
    public void ManualButton(string sceneName)
    {
        PlayerPrefs.SetInt(AUTOPLAYKEY, 0);
        ChangeToScene(sceneName);
    }
}
