using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceEvents : MonoBehaviour
{

    public static UserInterfaceEvents Singleton { private set; get; }

    public void Awake()
    {
        Singleton = this;
    }

    public event Action<bool> OnPauseMenu;

    public void PauseMenuTrigger(bool isPaused)
    {
        OnPauseMenu?.Invoke(isPaused);
    }

    public event Action<string> OnLookingAtItem;

    public void LookingAtItemTrigger(string objectName)
    {
        OnLookingAtItem?.Invoke(objectName);
    }

}
