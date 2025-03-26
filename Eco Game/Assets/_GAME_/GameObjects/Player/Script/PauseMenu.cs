using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shopMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            shopMenu.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void options ()
    {

    }

    public void restart ()
    {
        SceneManager.LoadScene(1);
    }

    public void home ()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void fullScreen ()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }


}
