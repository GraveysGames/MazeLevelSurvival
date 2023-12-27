using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Inventory : MonoBehaviour
{
    private Dictionary<Item, int> myInventory;

    private void Start()
    {
        InventoryEvents.Singleton.OnItemPickup += PickUpItem;
        InventoryEvents.Singleton.OnItemDropped += DropItem;
    }

    public Inventory()
    {
        myInventory = new();
    }

    public void PickUpItem(Item item)
    {
        myInventory[item] = myInventory.TryGetValue(item, out int value) ? ++value : 1;
        StatChangeEvents.Current.StatChangeTrigger(item.statTypes, item.statUpdateMethod, item.valueOfChange);
    }

    public void DropItem(Item item)
    {
        if (myInventory.TryGetValue(item, out int value))
        {
            if (value == 1)
            {
                myInventory.Remove(item);
            }
            else
            {
                myInventory[item] -= 1;
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
