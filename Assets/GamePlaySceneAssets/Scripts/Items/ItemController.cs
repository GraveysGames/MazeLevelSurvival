using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private static int itemSpawnCount = 1;
    [SerializeField] private Item _itemInfo;

    public int ItemId { private set; get; }

    private void Start()
    {
        ItemId = itemSpawnCount;
        itemSpawnCount++;
        MazeEvents.Singleton.OnItemDespawn += DespawnItem;
    }

    public string GetItemName()
    {
        return _itemInfo.name;
    }

    public Item PickUpItem()
    {
        Destroy(this.gameObject);
        return _itemInfo;
    }

    private void DespawnItem(int itemId)
    {
        if (ItemId == itemId)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        MazeEvents.Singleton.OnItemDespawn -= DespawnItem;
    }
}
