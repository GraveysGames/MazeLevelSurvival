using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerMenuController : MonoBehaviour
{
    public void Host()
    {
        LoadNextSceneVaraiables(true);
        SceneManager.LoadScene("Loading Next Scene");
    }
    public void Client()
    {
        LoadNextSceneVaraiables(false);
        SceneManager.LoadScene("Loading Next Scene");
    }

    private void LoadNextSceneVaraiables(bool mode)
    {
        GameManager.NetworkMode = mode;
        GameManager.Scene = "Test Multiplayer";
    }
}
