using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloPlayController : MonoBehaviour
{

    public void TutorialScene()
    {
        LoadNextSceneVaraiables(10, 3);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void EasyScene()
    {
        LoadNextSceneVaraiables(10, 10);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void MediumScene()
    {
        LoadNextSceneVaraiables(25, 25);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void HardScene()
    {
        LoadNextSceneVaraiables(50, 50);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void ExtremeScene()
    {
        LoadNextSceneVaraiables(100, 100);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void ImposibleScene()
    {
        LoadNextSceneVaraiables(1000, 1000);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void CustomScene(int width, int height)
    {
        LoadNextSceneVaraiables(width, height);
        SceneManager.LoadScene("Loading Next Scene");
    }

    public void EndlessSurvival()
    {
        LoadNextSceneVaraiables(50, 50);
        SceneManager.LoadScene("Loading Next Scene");
        GameManager.IsSurvival = true;
    }

    private void LoadNextSceneVaraiables(int width, int height)
    {
        GameManager.Width = width;
        GameManager.Height = height;
        GameManager.Scene = "SinglePlayer";
    }
}
