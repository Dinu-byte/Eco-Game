using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
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

        PlayerRespawn playerRespawn = FindAnyObjectByType<PlayerRespawn>();
        if (playerRespawn != null)
        {
            playerRespawn.Respawn();
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);

            // Enable all enemies when player respawns
            EnableEnemyEnemies();
            Debug.Log("Enabled enemies successfully.");

            gameOverCanvas.SetActive(false);

            PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
            playerHealth.setHealth();
        }
        else
        {
            Debug.LogError("PlayerRespawn script not found!");
        }


    }

    public void home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    // Method to disable all enemies tagged as "Enemy"
    public void DisableEnemyEnemies()
    {
        Time.timeScale = 0f;
    }

    // Method to enable all enemies tagged as "Enemy"
    private void EnableEnemyEnemies()
    {
        Time.timeScale = 1.0f;
    }
}
