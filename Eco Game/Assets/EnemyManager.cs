using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public EnemyRemainingBarScript enemyRemainingBarScript;

    private int totalEnemies;
    private int enemiesKilled;

    private void Start()
    {
        totalEnemies = 0;
        enemiesKilled = 0;
        totalEnemies = enemyCount();
        enemyRemainingBarScript.setMaxEnemyCount(totalEnemies);
        Debug.Log(totalEnemies);
    }

    public void checkEnemiesKilled()
    {
        if (enemiesKilled/100*90 >= totalEnemies)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void addEnemiesKilled()
    {
        enemiesKilled++;
        enemyRemainingBarScript.setEnemyCount(totalEnemies - enemiesKilled);
        checkEnemiesKilled();
    }

    public void addTotalEnemies()
    {
        totalEnemies++;
    }

    public int enemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public int getTotalEnemies()
    {
        return totalEnemies;
    }
}
