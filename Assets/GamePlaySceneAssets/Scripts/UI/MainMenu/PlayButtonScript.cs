using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{

    public void ChangeScene(bool Host)
    {
        GameManager.NetworkMode = Host;
        SceneManager.LoadScene("GamePlayScene");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }
}
