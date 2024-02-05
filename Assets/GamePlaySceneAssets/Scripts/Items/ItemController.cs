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
        SetUpObject();
    }

    private void SetUpObject()
    {
        if (!GetComponent<Collider>())
        {
            MeshCollider meshC = this.gameObject.AddComponent<MeshCollider>();
            meshC.convex = true;
        }

        if (!GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        

    }


    private float groundHeight = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundHeight = transform.position.y;
        }
    }

    private void LateUpdate()
    {
        if (transform.position.y < groundHeight)
        {
            transform.position = new(transform.position.x, groundHeight, transform.position.z);
        }
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
