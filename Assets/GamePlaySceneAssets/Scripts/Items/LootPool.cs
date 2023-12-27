using System;
using UnityEngine;


[CreateAssetMenu]

public class LootPool : ScriptableObject
{
    [SerializeField] private ItemsAndDropRate[] lootPool;
    private float chanceRange = -1;

    [Serializable]
    private struct ItemsAndDropRate
    {
        public Item[] Items;
        public float DropRate;

    }


    public Vector2 RollItemDrop()
    {
        Vector2 index = new(0,0);
        return index;
    }

    public Item GetItem(Vector3 Index)
    {
        return lootPool[(int)Index.x].Items[(int)Index.y];
    }

    public void PrintAllItems()
    {
        foreach (ItemsAndDropRate I in lootPool)
        {
            Debug.Log("Drop Rate: " + I.DropRate);
            foreach (Item thisItem in I.Items)
            {
                Debug.Log(thisItem.PrintItemInfo());
            }
        }
    }

}
