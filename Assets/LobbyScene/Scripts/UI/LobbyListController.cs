using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListController : MonoBehaviour
{

    [SerializeField] GameObject lobbyUIPrefab;

    private void Start()
    {
        ListLobbies();
    }

    public void ListLobbies()
    {

        InstantiateLobbyListing("this lobby", "DG2", 2, 4);
        InstantiateLobbyListing("a lobby", "a player", 1, 4);
        InstantiateLobbyListing("mylobby", "legend1", 4, 8);



    }

    private void InstantiateLobbyListing(string lobbyName, string hostName, int numberOfPlayers, int maxNumberOfplayers)
    {
        GameObject lobbyListing = Instantiate(lobbyUIPrefab, this.transform);
        lobbyListing.GetComponent<LobbyListingPrefabController>().SetText(lobbyName, hostName, numberOfPlayers, maxNumberOfplayers);
    }


}
