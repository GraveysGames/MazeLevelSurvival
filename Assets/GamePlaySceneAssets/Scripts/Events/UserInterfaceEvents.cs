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
}
