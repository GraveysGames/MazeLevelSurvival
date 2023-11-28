using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class PlayerInventoryController : MonoBehaviour
{
    public Dictionary<ItemController.ItemDetails, int> Inventory { get; private set; } 


    private void Start()
    {
        Inventory = new();

        GameEvents_Inventory.Current.OnItemPickup += ItemPickUp;
        GameEvents_Inventory.Current.OnItemDropped += ItemDropped;

    }

    private void OnDestroy()
    {
        GameEvents_Inventory.Current.OnItemPickup -= ItemPickUp;
        GameEvents_Inventory.Current.OnItemDropped -= ItemDropped;
    }

    private void ItemPickUp(ItemController.ItemDetails item)
    {
        AddItemToDict(item);
        Debug.Log("Inventory: picked up " + item.itemStats.name );
        GameEvents_Inventory.Current.DisplayInventoryTrigger(Inventory);
    }

    private void ItemDropped(ItemController.ItemDetails item)
    {
        RemoveItemFromDict(item);
        GameEvents_Inventory.Current.DisplayInventoryTrigger(Inventory);
    }



    #region utility functions

    private void AddItemToDict(ItemController.ItemDetails item)
    {
        if (Inventory.ContainsKey(item))
        {
            Inventory[item] += 1;
        }
        else
        {
            Inventory.Add(item, 1);
        }
    }

    private void RemoveItemFromDict(ItemController.ItemDetails item)
    {
        if (Inventory.ContainsKey(item))
        {
            Inventory[item] -= 1;

            if (Inventory[item] < 1)
            {
                Inventory.Remove(item);
            }
        }
    }


    #endregion
}
*/