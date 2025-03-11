using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayButton ()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitButton ()
    {
        Application.Quit();
    }
}
