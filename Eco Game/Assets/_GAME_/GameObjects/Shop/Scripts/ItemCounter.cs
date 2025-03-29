using UnityEngine;

public class ItemCounter
{
    private int counter;
    private bool canBeBought;
    private int maxTimesBought;

    public ItemCounter ()
    {
        counter = 0;
        canBeBought = true;
        maxTimesBought = 8;
    }

    public void addCounter ()
    {
        counter++;
    }

    public int getCounter ()
    {
        return counter;
    }

    public bool getCanBeBought ()
    {
        return canBeBought; 
    }

    public void checkTimesBought () 
    {
        if (counter >= maxTimesBought)
        {
            canBeBought = false;
        }
    }
}
