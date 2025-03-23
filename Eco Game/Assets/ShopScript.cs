using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;
    private GameObject player;

    private bool isColliding;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            if (shopMenu.activeSelf)
            {
                shopMenu.SetActive(false);
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                shopMenu.SetActive(true);
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) isColliding = false;
        shopMenu.SetActive(false);
    }
}
