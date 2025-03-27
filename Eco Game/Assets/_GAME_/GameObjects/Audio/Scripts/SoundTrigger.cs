using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private AudioManager audioManager;
    private bool hasPlayed;

    public AudioClip sound;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        hasPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayed)
        {
            audioManager.playSFX(sound);
            hasPlayed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) hasPlayed = false;
    }
}
