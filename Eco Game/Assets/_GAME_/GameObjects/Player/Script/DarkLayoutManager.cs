using UnityEngine;
using UnityEngine.Tilemaps;

public class DarkLayoutManager : MonoBehaviour
{
    public Tilemap[] insideTilemaps;   // Array of Tilemaps for the Inside area (walls, ground, etc.)
    public Tilemap[] outsideTilemaps;  // Array of Tilemaps for the Outside area (grass, water, etc.)
    public Tilemap darkLayoutTilemap;  // Reference to the DarkLayout tilemap
    public Tilemap roofTilemap;

    private bool wasInsideLastFrame = false;  // Track if the player was inside on the last frame
    private float cooldownTimer = 0f;  // Timer to manage cooldown
    private float cooldownDuration = 0.3f;  // Cooldown duration in seconds

    private void Start()
    {
        // Initially deactivate the dark layout tilemap
        darkLayoutTilemap.gameObject.SetActive(false);
        roofTilemap.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Update the cooldown timer
        cooldownTimer -= Time.deltaTime;

        // Check the player's position to determine if inside or outside
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        // If we're still in cooldown, skip processing
        if (cooldownTimer > 0f)
            return;

        Vector3 playerPosition = transform.position;

        bool playerOnInsideTile = false;
        bool playerOnOutsideTile = false;

        // Convert the player's world position to cell position for each tilemap
        foreach (Tilemap tilemap in insideTilemaps)
        {
            Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);

            if (tilemap.HasTile(playerCellPosition))  // Check if player is on any tile in Inside layer
            {
                playerOnInsideTile = true;
                break;
            }
        }

        foreach (Tilemap tilemap in outsideTilemaps)
        {
            Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);

            if (tilemap.HasTile(playerCellPosition))  // Check if player is on any tile in Outside layer
            {
                playerOnOutsideTile = true;
                break;
            }
        }

        // If the player enters the Inside area
        if (playerOnInsideTile && !wasInsideLastFrame)
        {
            darkLayoutTilemap.gameObject.SetActive(true);// Activate DarkLayout
            roofTilemap.gameObject.SetActive(false);  //Attiva roof
            wasInsideLastFrame = true;
            Debug.Log("Player entered inside");

            // Start cooldown to prevent flickering
            cooldownTimer = cooldownDuration;
        }
        // If the player enters the Outside area
        else if (playerOnOutsideTile && wasInsideLastFrame && !playerOnInsideTile)
        {
            darkLayoutTilemap.gameObject.SetActive(false);  // Deactivate DarkLayout
            roofTilemap.gameObject.SetActive(true);  //Attiva roof
            wasInsideLastFrame = false;
            Debug.Log("Player entered outside");

            // Start cooldown to prevent flickering
            cooldownTimer = cooldownDuration;
        }
    }
}
