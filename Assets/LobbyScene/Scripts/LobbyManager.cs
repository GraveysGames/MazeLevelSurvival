using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


    private void Update()
    {
        if (TenSecondInterval())
        {

        }
    }

    float interval = 10f;
    float timeSince = 10f;
    private bool TenSecondInterval()
    {
        if (timeSince < 0)
        {
            timeSince = interval;
            return true;
        }
        else
        {
            timeSince -= Time.deltaTime;
            return false;
        }
    }


    private async void CreateLobby(string lobbyName, int maxPlayers)
    {
        try
        {
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

}
