using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceEvents : MonoBehaviour
{

    public static UserInterfaceEvents Current { private set; get; }

    public void Awake()
    {
        Current = this;
    }

    public event Action OnPauseButtonDown;

    public void OnPauseButtonDownTrigger()
    {
        OnPauseButtonDown?.Invoke();
    }
}
