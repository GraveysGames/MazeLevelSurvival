using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Enemy_ItemDrop : MonoBehaviour
{
    [SerializeField] private LootPool lootPool;
    [SerializeField] private float chanceToDropItem;
    [SerializeField] private Transform itemContainer;
    public void ProcItemDrop()
    {
        float rChance = Random.Range(0f, 100f);

        if (rChance < chanceToDropItem)
        {
            Vector2 droppedItemIndex = lootPool.RollItemDrop();
            MultiplayerItemSpawning.Singleton.DropItem(droppedItemIndex, lootPool, this.transform.position);
            
        }
    }
}
