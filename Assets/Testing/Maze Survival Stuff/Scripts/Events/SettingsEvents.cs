using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsEvents : MonoBehaviour
{

    public static SettingsEvents Current { private set; get; }


    private void Awake()
    {
        Current = this;
    }

    public event Action OnGameInputButtonChanged;

    public void OnGameInputButtonChangedTrigger()
    {
        OnGameInputButtonChanged?.Invoke();
    }

}
