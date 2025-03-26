using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    private void Start()
    {
        Screen.fullScreen = true;
    }
    public void PlayButton ()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitButton ()
    {
        Application.Quit();
    }
}
