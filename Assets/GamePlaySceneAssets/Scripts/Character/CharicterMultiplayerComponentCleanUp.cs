using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharicterMultiplayerComponentCleanUp : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            this.enabled = false;
        }
        else
        {
            Inventory I = GetComponent<Inventory>();
            if (I != null)
            {
                I.enabled = false;
            }
            CapsuleCollider c = GetComponent<CapsuleCollider>();
            if (c != null)
            {
                c.enabled = false;
            }
            CharacterController charCont = GetComponent<CharacterController>();
            if (charCont != null)
            {
                charCont.enabled = false;
            }
            PlayerInteractionController inter = GetComponent<PlayerInteractionController>();
            if (inter != null)
            {
                inter.enabled = false;
            }
            CharicterStatsController statsController = GetComponent<CharicterStatsController>();
            if (statsController != null)
            {
                statsController.enabled = false;
            }
        }
    }
}
