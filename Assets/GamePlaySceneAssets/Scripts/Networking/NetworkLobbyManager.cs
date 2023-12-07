using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkLobbyManager : NetworkBehaviour
{

    [SerializeField] NetworkManager netManager;


    // Start is called before the first frame update
    void Start()
    {



        if (GameManager.NetworkMode)
        {
            netManager.StartHost();
        }
        else
        {
            netManager.StartClient();
        }
        
    }

}
