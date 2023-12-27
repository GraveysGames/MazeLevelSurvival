using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    private GameObject _itemBeingLookedAt;
    private GameObject _previousItemLookedAt;
    private string _itemBeingLookedAtName;

    [SerializeField] private float _interactionRange = 10f;
    [SerializeField] private float _sphereColiderSize = 0.1f;

    private void Start()
    {
        InvokeRepeating(nameof(CheckForInteractableObjects), 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_itemBeingLookedAt != null)
        {
            if (Input.GetKeyDown(KeyCode.F) == true)
            {
                InteractWithObject();
            }
        }
    }

    private void CheckForInteractableObjects()
    {
        if (Camera.main == null)
        {
            return;
        }
        RaycastHit raycastHit;
        if (Physics.SphereCast(Camera.main.transform.position, _sphereColiderSize, Camera.main.transform.forward, out raycastHit, _interactionRange, LayerMask.GetMask("Items", "MazeInteractions")))
        {
            if (raycastHit.collider != null)
            {
                _itemBeingLookedAt = raycastHit.transform.gameObject;
                FindItemName();
            }
            else
            {
                _itemBeingLookedAt = null;
                _itemBeingLookedAtName = null;
            }
        }
        else
        {
            _itemBeingLookedAt = null;
            _itemBeingLookedAtName = null;
        }

        if (_itemBeingLookedAt != _previousItemLookedAt)
        {
            UserInterfaceEvents.Singleton.LookingAtItemTrigger(_itemBeingLookedAtName);
            _previousItemLookedAt = _itemBeingLookedAt;
        }

        

    }

    
    private void InteractWithObject()
    {
        ItemController IC = _itemBeingLookedAt.GetComponent<ItemController>();
        if (IC != null)
        {
            Item itemPickedUp = IC.PickUpItem();
            MultiplayerItemSpawning.Singleton.DespawnLoot(IC.ItemId);
            InventoryEvents.Singleton.ItemPickupTrigger(itemPickedUp);
            UserInterfaceEvents.Singleton.LookingAtItemTrigger(null);
        }

        PortalController PC = _itemBeingLookedAt.GetComponent<PortalController>();
        if (PC != null)
        {
            PC.GoThroughPortal();
            UserInterfaceEvents.Singleton.LookingAtItemTrigger(null);
        }

    }

    private void FindItemName()
    {
        if (_itemBeingLookedAtName == null)
        {
            ItemController IC = _itemBeingLookedAt.GetComponent<ItemController>();
            if (IC != null)
            {
                _itemBeingLookedAtName = IC.GetItemName();
                //GetComponent<PlayerHudInteraction>().DisplayInteraction(_itemBeingLookedAtName);
            }

            PortalController PC = _itemBeingLookedAt.GetComponent<PortalController>();
            if (PC != null)
            {
                _itemBeingLookedAtName = PC.ObjectName;
                //GetComponent<PlayerHudInteraction>().DisplayInteraction(_itemBeingLookedAtName);
            }

        }
    }

}
