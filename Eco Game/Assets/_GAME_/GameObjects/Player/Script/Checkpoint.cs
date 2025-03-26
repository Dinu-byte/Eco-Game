// Checkpoint.cs
using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    private bool isOnCooldown = false;
    public float cooldownTime = 120f; // 2 minutes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            CheckpointManager.Instance.SetCheckpoint(transform.position);
            StartCoroutine(Cooldown());
            Debug.Log("Checkpoint brat");
        }
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
