using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MultiplayerItemSpawning : NetworkBehaviour
{

    [SerializeField] private LootPool[] allLootPools;

    public static MultiplayerItemSpawning Singleton { private set; get; }
    private void Awake()
    {
        Singleton = this;
    }

    public void DropItem(Vector2 droppedItemIndex, LootPool lootPool, Vector3 position)
    {
        int lootPoolIndex = 0;
        foreach (LootPool pool in allLootPools)
        {
            if (pool == lootPool)
            {
                break;
            }
            lootPoolIndex++;
        }
        DropLootClientRPC(lootPoolIndex, droppedItemIndex, position);
    }


    private void DropLoot(int lootPoolIndex, Vector2 droppedItemIndex, Vector3 position)
    {
        GameObject item = Instantiate(allLootPools[lootPoolIndex].GetItem(droppedItemIndex).itemPrefab, position, new Quaternion(0, 0, 0, 0));
        //Debug.Log(position);
        //item.GetComponent<NetworkObject>().Spawn();
        //item.transform.position = this.transform.position;
    }

    [ClientRpc]
    public void DropLootClientRPC(int lootPoolIndex, Vector2 droppedItemIndex, Vector3 position)
    {
        DropLoot(lootPoolIndex, droppedItemIndex, position);
    }


    public void ItemDespawn(int itemId)
    {
        MazeEvents.Singleton.ItemDespawnTrigger(itemId);
    }

    public void DespawnLoot(int itemId)
    {
        if (IsHost)
        {
            DespawnLootClientRPC(itemId);
        }
        else
        {
            DespawnLootRelayServerRPC(itemId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnLootRelayServerRPC(int itemId)
    {
        DespawnLootClientRPC(itemId);
    }


    [ClientRpc]
    public void DespawnLootClientRPC(int itemId)
    {
        ItemDespawn(itemId);
    }
}
