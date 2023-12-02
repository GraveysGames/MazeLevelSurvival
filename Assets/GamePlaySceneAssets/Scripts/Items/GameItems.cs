using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GameItems : ScriptableObject
{
    public List<Item> Items;


    [Serializable]
    public struct Item
    {
        [SerializeField] public string itemName;
        [SerializeField] public CharicterStats.StatLog[] statTypes;
        [SerializeField] public CharicterStats.MethodOfUpdatingStats[] statUpdateMethod;
        [SerializeField] public float[] valueOfChange;
        [SerializeField] public Sprite ItemSprite;
        [SerializeField] public GameObject itemObject;
    }


    public void PrintAllItems()
    {
        foreach (Item thisItem in Items)
        {
            String ItemStats = thisItem.itemName + "\n";
            for (int i = 0; i < thisItem.statTypes.Length; i++)
            {
                ItemStats += "\t" + "Stat " + i + ": \n";
                ItemStats += "\t\tStat Type: " + thisItem.statTypes[i] + "\n\t\tUpdate Method: " + thisItem.statUpdateMethod[i] + "\n\t\tValue Of Change: " + thisItem.valueOfChange[i] + "\n";
            }
            Debug.Log(ItemStats);
        }
    }

}
