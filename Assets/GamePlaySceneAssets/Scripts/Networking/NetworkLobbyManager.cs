using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkLobbyManager : NetworkBehaviour
{

    [SerializeField] NetworkManager netManager;
    [SerializeField] EndGame endGame;


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

        NetworkManager.Singleton.OnClientStopped += DiscontectedFromHost;

    }

    public static void DisconnectNetwork()
    {
        NetworkManager.Singleton.Shutdown();

        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }

    private void DiscontectedFromHost(bool status)
    {
        if (true)
        {
            endGame.GameEnded();
        }
        
    }

}
