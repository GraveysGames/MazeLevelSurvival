using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AllItems : ScriptableObject
{
    [SerializeField] public ItemStruct[] ItemPrefabList;

    [System.Serializable]
    public struct ItemStruct
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public ItemRarity rarity;
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Unfindable
    }

    public void SpawnItem(Vector3 spawnPosition, Transform itemContainer)
    {
        GameObject item = Instantiate(ItemPrefabList[0].prefab, itemContainer);
        item.transform.position = spawnPosition;
    }

}
