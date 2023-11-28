using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerInteractionController : MonoBehaviour
{

    private GameObject _itemBeingLookedAt;
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
        if (Camera.current == null)
        {
            return;
        }
        RaycastHit raycastHit;
        if (Physics.SphereCast(Camera.current.transform.position, _sphereColiderSize, Camera.current.transform.forward, out raycastHit, _interactionRange, LayerMask.GetMask("InteractableObjects")))
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

        GetComponent<PlayerHudInteraction>().DisplayInteraction(_itemBeingLookedAtName);

    }

    
    private void InteractWithObject()
    {
        ItemController IC = _itemBeingLookedAt.GetComponent<ItemController>();
        if (IC != null)
        {
            ItemController.ItemDetails itemPickedUp = IC.PickUpItem();
            GameEvents_Inventory.Current.ItemPickupTrigger(itemPickedUp);
        }

        PortalController PC = _itemBeingLookedAt.GetComponent<PortalController>();
        if (PC != null)
        {
            PC.GoThroughPortal();
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
                GetComponent<PlayerHudInteraction>().DisplayInteraction(_itemBeingLookedAtName);
            }

            PortalController PC = _itemBeingLookedAt.GetComponent<PortalController>();
            if (PC != null)
            {
                _itemBeingLookedAtName = PC.ObjectName;
                GetComponent<PlayerHudInteraction>().DisplayInteraction(_itemBeingLookedAtName);
            }

        }
    }

}
*/