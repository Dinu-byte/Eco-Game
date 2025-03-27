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
    private ItemCounter[] checks;

    public float costMultiplier;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        costs = new int[itemsSO.Length];
        checks = new ItemCounter[itemsSO.Length];
        for (int i = 0; i < itemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i] = new ItemCounter();
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
            
                if (player.GetComponent<PlayerHealth>().coins >= costs[i] && checks[i].getCanBeBought())
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
            checks[btnNo].addCounter();
            checks[btnNo].checkTimesBought();
            checkPurchaseable();
        }
    }
}
