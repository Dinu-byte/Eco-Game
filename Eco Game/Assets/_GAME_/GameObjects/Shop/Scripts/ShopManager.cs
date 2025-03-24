using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private GameObject player;
    public ItemSO[] itemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButtons;
    private int[] costs;

    public float costMultiplier;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        costs = new int[itemsSO.Length];
        for (int i = 0; i < itemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        loadCosts();
        loadPanels();
        checkPurchaseable();
    }

    public void loadPanels ()
    {
        for (int i = 0; i < itemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = itemsSO[i].title;
            shopPanels[i].descriptionTxt.text = itemsSO[i].description;
            shopPanels[i].costTxt.text = "Cost: " + costs[i];
        }
    }

    private void loadCosts () // called ONLY at the Start.
    {
        for (int i = 0; i < itemsSO.Length; i++)
        {
            costs[i] = itemsSO[i].baseCost;
        }
    }

    public void checkPurchaseable ()
    {
        for (int i = 0; i < itemsSO.Length; i++)
        {
            if (player.GetComponent<PlayerHealth>().coins >= costs[i])
            {
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    private void loadCost (int index)
    {
        shopPanels[index].costTxt.text = "Cost: " + costs[index];
    }

    public void purchaseItem (int btnNo)
    {
        if (player.GetComponent<PlayerHealth>().coins >= costs[btnNo])
        {
            player.GetComponent<PlayerHealth>().addCoins(-costs[btnNo]);
            costs[btnNo] = Mathf.RoundToInt(costs[btnNo] * costMultiplier); // when you upgrade, the cost gets higher.
            loadCost(btnNo);
            checkPurchaseable();
        }
    }
}
