using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterInventory : MonoBehaviour
{
    [SerializeField] GameItems ItemsInGame;

    private void Start()
    {
        ItemsInGame.PrintAllItems();
    }
}
