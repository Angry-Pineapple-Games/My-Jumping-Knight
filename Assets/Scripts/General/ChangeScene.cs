using UnityEngine;
using UnityEngine.SceneManagement;

/*Proporciona un script que puede ser aderido a cualquier objeto, con el fin
 de posibilitar un cambio de escena*/
public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
