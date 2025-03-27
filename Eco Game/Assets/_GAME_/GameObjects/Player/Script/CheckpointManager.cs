// CheckpointManager.cs
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    public Vector2 LastCheckpoint { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void SetCheckpoint(Vector2 position)
    {
        LastCheckpoint = position;
    }
}

