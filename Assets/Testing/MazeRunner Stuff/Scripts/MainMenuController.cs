using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject ExitPopup;
    [SerializeField] GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void QuitGame()
    {
        //if (UnityEditor.EditorApplication.isPlaying)
        //{
        //    UnityEditor.EditorApplication.isPlaying = false;
        //}
        //else
        //{
            Application.Quit();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeKeyPressed();
        }
    }

    private void EscapeKeyPressed()
    {

        if (StartMenu.activeSelf)
        {
            if (ExitPopup.activeSelf)
            {
                ExitPopup.SetActive(false);
            }
            else
            {
                ExitPopup.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            background.SetActive(true);
            StartMenu.SetActive(true);

        }


    }

}
