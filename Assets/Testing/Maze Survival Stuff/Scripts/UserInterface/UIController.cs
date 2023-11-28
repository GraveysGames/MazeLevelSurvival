using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    private KeyCode pauseButton;


    void Start()
    {
        GetUIButtons();
        SettingsEvents.Current.OnGameInputButtonChanged += GetUIButtons;
    }

    private void GetUIButtons()
    {
        pauseButton = GameInputs.PauseButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            UserInterfaceEvents.Current.OnPauseButtonDownTrigger();
        }
    }
}
