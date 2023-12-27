using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{

    public static InventoryEvents Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
    }

    public Action<Item> OnItemPickup;

    public void ItemPickupTrigger(Item item)
    {
        OnItemPickup?.Invoke(item);
    }

    public Action<Item> OnItemDropped;

    public void ItemDroppedTrigger(Item item)
    {
        OnItemDropped?.Invoke(item);
    }

}
