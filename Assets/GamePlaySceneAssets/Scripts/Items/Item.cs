using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public CharicterStats.StatLog[] statTypes;
    public CharicterStats.MethodOfUpdatingStats[] statUpdateMethod;
    public float[] valueOfChange;
    public Sprite ItemSprite;
    public GameObject itemPrefab;




    public string PrintItemInfo()
    {
        string output = "";
        for (int i = 0; i < statTypes.Length; i++)
        {
            output += "\t" + "Stat " + i + ": \n";
            output += "\t\tStat Type: " + statTypes[i] + "\n\t\tUpdate Method: " + statUpdateMethod[i] + "\n\t\tValue Of Change: " + valueOfChange[i] + "\n\t\t" + itemPrefab + "\n\t\t" + ItemSprite + "\n";
        }
        return output;
    }


}
