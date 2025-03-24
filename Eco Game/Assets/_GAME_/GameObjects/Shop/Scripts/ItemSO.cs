using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/NewShopItem", order = 1)]
public class ItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
}
