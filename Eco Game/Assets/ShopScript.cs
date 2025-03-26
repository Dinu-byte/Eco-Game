using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject shopPanel;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (shopMenu.activeSelf)
            {
                shopMenu.SetActive(false);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                audioManager.playSFX(audioManager.SFX_UI_back);
            }
            else
            {
                shopMenu.SetActive(true);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                shopPanel.GetComponent<ShopManager>().checkPurchaseable();
                audioManager.playSFX(audioManager.SFX_UI_select);
            }
        }

    }

}
