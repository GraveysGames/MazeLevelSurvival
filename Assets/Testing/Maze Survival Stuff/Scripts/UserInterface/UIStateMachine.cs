using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMachine : MonoBehaviour
{

    private enum UIState
    {
        PlayingGame,
        MainPauseMenu
    }

    private UIState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = UIState.PlayingGame;
        SetUpEvents();
    }

    private void SetUpEvents()
    {
        UserInterfaceEvents.Current.OnPauseButtonDown += ChangePausedValue;
    }

    private bool isPaused = false;
    private void ChangePausedValue()
    {
        if (isPaused == true)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }

    private void PlayingGame()
    {
        if (isPaused)
        {
            currentState = UIState.MainPauseMenu;
        }
    }

    bool runFlag = false;
    private void GamePaused()
    {
        if (isPaused == false)
        {
            currentState = UIState.PlayingGame;
            GameFlowEvents.Current.OnPauseTrigger(isPaused);
            CloseAllPauseMenus();
            runFlag = false;
            return;
        }


        if (runFlag == false)
        {
            GameFlowEvents.Current.OnPauseTrigger(isPaused);
            runFlag = true;
        }

    }

    private void CloseAllPauseMenus()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case UIState.PlayingGame:
                PlayingGame();
                break;
            case UIState.MainPauseMenu:
                GamePaused();
                //Debug.Log("UIState Paused");
                break;
        }
    }
}
