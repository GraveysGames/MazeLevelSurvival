using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public Dictionary<GameItems.Item, int> gameItems { protected set; get; }

    public Inventory()
    {
        gameItems = new();
    }

    public void AddItemToInventory(GameItems.Item item)
    {
        gameItems[item] = gameItems.TryGetValue(item, out int value) ? ++value : 1;
        StatChangeEvents.Current.StatChangeTrigger(item.statTypes, item.statUpdateMethod, item.valueOfChange);
    }

    public void RemoveItemToInventory(GameItems.Item item)
    {
        if (gameItems.TryGetValue(item, out int value))
        {
            if (value == 1)
            {
                gameItems.Remove(item);
            }
            else
            {
                gameItems[item] -= 1;
            }
        }

        float[] newValueOfChange = new float[item.valueOfChange.Length];

        for (int i = 0; i < item.valueOfChange.Length; i++)
        {
            newValueOfChange[i] = -1 * item.valueOfChange[i];
        }

        StatChangeEvents.Current.StatChangeTrigger(item.statTypes, item.statUpdateMethod, newValueOfChange);

    }

}
