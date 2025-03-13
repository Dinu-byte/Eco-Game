using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void options ()
    {

    }
    public void home ()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

}
