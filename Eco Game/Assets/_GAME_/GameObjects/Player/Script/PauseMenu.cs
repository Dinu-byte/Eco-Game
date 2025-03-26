using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shopMenu;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            shopMenu.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void options()
    {
        // You can add options menu functionality here
    }

    public void restart()
    {
        Debug.Log("Trying to find PlayerRespawn script...");

        PlayerRespawn playerRespawn = FindObjectOfType<PlayerRespawn>();
        if (playerRespawn != null)
        {
            playerRespawn.Respawn();
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("PlayerRespawn script not found!");
        }

        SceneManager.LoadScene(0);
    }

    public void home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}
