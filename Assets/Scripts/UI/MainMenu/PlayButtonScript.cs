using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{

    public static void ChangeScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
}
