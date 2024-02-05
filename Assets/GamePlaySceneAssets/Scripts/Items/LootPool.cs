using System;
using UnityEngine;


[CreateAssetMenu]

public class LootPool : ScriptableObject
{
    [SerializeField] private ItemsAndDropRate[] lootPool;
    private float _chanceRange = 0;
    private bool _isRangeCalculated = false;

    [Serializable]
    private struct ItemsAndDropRate
    {
        public Item[] Items;
        public float DropRate;

    }

    private void calculateRange()
    {
        foreach (ItemsAndDropRate IADR in lootPool)
        {
            _chanceRange = IADR.Items.Length * IADR.DropRate;
        }
    }

    public Vector2 RollItemDrop()
    {
        if (_isRangeCalculated == false)
        {
            calculateRange();
        }

        Vector2 index = new(0, 0);

        float randomNumber = UnityEngine.Random.Range(0f,_chanceRange);

        foreach (ItemsAndDropRate IADR in lootPool)
        {
            float thisChanceRange = IADR.Items.Length * IADR.DropRate;
            if (thisChanceRange > randomNumber)
            {
                index.y = (int)(randomNumber / IADR.DropRate);
                
            }
            else
            {
                randomNumber -= thisChanceRange;
                index.x++;
            }
        }
        Debug.Log("RandomNumber: " + randomNumber + " Chance Range: " + _chanceRange + " index: " + index.x + " " + index.y);
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
