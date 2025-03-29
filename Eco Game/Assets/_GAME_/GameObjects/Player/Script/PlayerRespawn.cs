// PlayerRespawn.cs
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void Respawn()
    {
        if (CheckpointManager.Instance.LastCheckpoint != Vector2.zero)
        {
            transform.position = CheckpointManager.Instance.LastCheckpoint;
        }
        else
        {
            transform.position = initialPosition;
        }

        GetComponent<PlayerHealth>().setToMaxHealth();

        GetComponent<PlayerMovement>().enabled = true;
    }
}