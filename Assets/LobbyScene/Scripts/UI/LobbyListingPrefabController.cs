using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyListingPrefabController : MonoBehaviour
{
    [SerializeField] TMP_Text serverNameText;
    [SerializeField] TMP_Text hostNameText;
    [SerializeField] TMP_Text playerCountTextNameText;


    public void SetText(string serverName, string hostName, int playerCount, int maxPlayerCount)
    {
        serverNameText.text = serverName;
        hostNameText.text = hostName;
        playerCountTextNameText.text = playerCount + " / " + maxPlayerCount;
    }


}
