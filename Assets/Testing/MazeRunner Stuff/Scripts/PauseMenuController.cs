using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] GameObject MainPauseMenu;
    [SerializeField] GameObject PauseMenuContainer;
    [SerializeField] GameObject BackGround;
    [SerializeField] GameObject WinMenuObj;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ExitPopup;

    // Start is called before the first frame update
    void Start()
    {
        //GameEvents_SinglePlayer.current.OnWinTrigger += WinMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!WinMenuObj.activeSelf)
            {
                PauseUnpause();
            }
            
        }
    }

    /// <summary>
    /// used to manage curser and other things when you enter a menu and leave
    /// Call when entering and leaving a menu
    /// </summary>
    /// <param name="newState">bool: true in menu, false out of menu</param>
    private void InMenu(bool newState)
    {
        if (newState)
        {
            //Makes it invisable
            Cursor.visible = true;
            //Locks the mouse in place
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Makes it invisable
            Cursor.visible = false;
            //Locks the mouse in place
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PauseUnpause()
    {

        if (MainPauseMenu.activeSelf)
        {
            InMenu(false);

            BackGround.SetActive(false);
            MainPauseMenu.SetActive(false);
            ExitPopup.SetActive(false);
        }
        else if (BackGround.activeSelf)
        {

            MainPauseMenu.SetActive(true);
            SettingsMenu.SetActive(false);
            
        }
        else
        {
            InMenu(true);


            BackGround.SetActive(true);
            MainPauseMenu.SetActive(true);
            
        }

    }

    public void ExitScene()
    {
        GameManager.Scene = "Menu";

        SceneManager.LoadScene("Loading Next Scene");
    }

    private void WinMenu()
    {
        InMenu(true);
        BackGround.SetActive(true);
        WinMenuObj.SetActive(true);

    }

}
