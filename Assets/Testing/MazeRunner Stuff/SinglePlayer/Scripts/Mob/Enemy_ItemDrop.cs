using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ItemDrop : MonoBehaviour
{
    [SerializeField] private AllItems items;

    [Range(0f,100f)]
    [SerializeField] private float _itemDropChance;
    public void OnDeath(Transform itemContainer)
    {
        float roll = (float)Random.Range(0,10000) / 100f;
        if (roll < _itemDropChance)
        {
            items.SpawnItem(transform.position, itemContainer);
        }
    }
}
