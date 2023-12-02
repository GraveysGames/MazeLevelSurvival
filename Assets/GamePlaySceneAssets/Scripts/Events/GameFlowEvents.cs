using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowEvents : MonoBehaviour
{

    public static GameFlowEvents Current { private set; get;}

    private void Awake()
    {
        Current = this;
    }

    public event Action<bool> OnPause;

    public void OnPauseTrigger(bool isPaused)
    {
        OnPause?.Invoke(isPaused);
    }
}
