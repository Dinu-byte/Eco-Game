using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleTilemaps : MonoBehaviour
{
    public Tilemap tilemap1;
    public Tilemap tilemap2;
    public float toggleInterval = 1f;

    private float timer;

    void Start()
    {
        if (tilemap1 == null || tilemap2 == null)
        {
            Debug.LogError("Assign both tilemaps in the inspector.");
            enabled = false;
            return;
        }
        
        tilemap1.gameObject.SetActive(true);
        tilemap2.gameObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= toggleInterval)
        {
            tilemap1.gameObject.SetActive(!tilemap1.gameObject.activeSelf);
            tilemap2.gameObject.SetActive(!tilemap2.gameObject.activeSelf);
            timer = 0f;
        }
    }
}