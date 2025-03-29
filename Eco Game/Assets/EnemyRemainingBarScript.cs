using UnityEngine;
using UnityEngine.UI;

public class EnemyRemainingBarScript : MonoBehaviour
{
    public Slider slider;

    public void setMaxEnemyCount(float enemyCount)
    {
        slider.maxValue = enemyCount;
        slider.value = enemyCount;
    }

    public void setEnemyCount(float currentEnemyCount)
    {
        slider.value = currentEnemyCount;
    }
}
