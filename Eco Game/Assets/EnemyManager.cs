using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private int totalEnemies;
    private int enemiesKilled;

    private void Start()
    {
        totalEnemies = 0;
        enemiesKilled = 0;
        totalEnemies = enemyCount();
        Debug.Log(totalEnemies);
    }

    public void checkEnemiesKilled()
    {
        if (enemiesKilled >= totalEnemies)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void addEnemiesKilled ()
    {
        enemiesKilled++;
    }

    public void addTotalEnemies ()
    {
        totalEnemies++;
    }

    public int enemyCount ()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public int getTotalEnemies ()
    {
        return totalEnemies;
    }
}
