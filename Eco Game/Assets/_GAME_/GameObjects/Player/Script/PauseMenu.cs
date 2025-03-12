using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool notActive;
    private float timer;
    public float coolDown;
    void Start()
    {
        notActive = true;
        timer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= coolDown)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (notActive)
                {
                    pauseMenu.SetActive(true);
                    notActive = false;
                }
                else if (!notActive)
                {
                    pauseMenu.SetActive(false);
                    notActive = true;
                }
                timer = 0;
            }
            
        }
        else
        {
            timer += Time.deltaTime;
        }


    }

    public void resume ()
    {
        pauseMenu.SetActive(false);
        notActive = true;
    }
    public void options ()
    {

    }
    public void home ()
    {
        SceneManager.LoadScene(0);
    }

}
