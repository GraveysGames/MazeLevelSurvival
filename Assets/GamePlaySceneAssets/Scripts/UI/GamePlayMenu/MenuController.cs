using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject backGround;

    public enum UIState
    {
        PlayingGame,
        InMenu
    }

    UIState currentState = UIState.PlayingGame;

    public void Update()
    {
        UpdateCases();
    }


    private void UpdateCases()
    {
        switch (currentState)
        {
            case UIState.PlayingGame:

                if (Input.GetKeyDown(GameInputs.PauseButton))
                {
                    OpenPauseMenu();
                }

                break;
            case UIState.InMenu:
                if (Input.GetKeyDown(GameInputs.PauseButton))
                {
                    if (mainMenu.activeSelf)
                    {
                        ClosePauseMenu();
                    }
                }
                break;
        }
    }

    public void OpenPauseMenu()
    {
        UserInterfaceEvents.Singleton.PauseMenuTrigger(true);
        currentState = UIState.InMenu;
        ShowMainMenu();
    }

    public void ClosePauseMenu()
    {
        UserInterfaceEvents.Singleton.PauseMenuTrigger(false);
        currentState = UIState.PlayingGame;
        HideMainMenu();
    }

    private void HideMainMenu()
    {
        mainMenu.SetActive(false);
        backGround.SetActive(false);
    }

    private void ShowMainMenu()
    {
        mainMenu.SetActive(true);

        if (!backGround.activeSelf)
        {
            backGround.SetActive(true);
        }
    }
}
