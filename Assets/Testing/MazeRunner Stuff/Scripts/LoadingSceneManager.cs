using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{

    [SerializeField] TMP_Text loadingProgressText;
    [SerializeField] GameObject FinishedLoadingObject;
    [SerializeField] TMP_Text FinishedLoadingText;

    //AsynchronousOperation loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

    private string nextScene = "";

    private AsyncOperation asyncManager;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = GameManager.Scene;
        asyncManager = SceneManager.LoadSceneAsync(nextScene);
        asyncManager.allowSceneActivation = false;
        //AsynchronousLoad(nextScene);
    }

    private void Update()
    {
        AsynchronousLoad();
    }

    private void AsynchronousLoad()
    {

        if (!asyncManager.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(asyncManager.progress / 0.9f);
            loadingProgressText.text = "" + (progress * 100) + "%";
            //Debug.log("Loading progress: " + (progress * 100) + "%");
            // Loading completed
            if (asyncManager.progress > 0.89f)
            {
                //Debug.Log("Press a key to start");

                if (nextScene == "Menu")
                {
                    asyncManager.allowSceneActivation = true;
                }
                else
                {
                    FinishedLoadingObject.SetActive(true);
                    FinishedLoadingText.text = "Press a key to start";

                    if (Input.GetKeyDown(KeyCode.Space))
                        asyncManager.allowSceneActivation = true;
                }
            }
        }

    }

}
