using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangeEvents : MonoBehaviour
{
    public static StatChangeEvents Current { private set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(CallAllUpdatingFunctions), 1f, 1.0f);
    }

    private void CallAllUpdatingFunctions()
    {
        StatUpdateTrigger();
    }

    public event Action<CharicterStats.StatLog[], CharicterStats.MethodOfUpdatingStats[], float[]> OnStatChange;

    public void StatChangeTrigger(CharicterStats.StatLog[] statToChange, CharicterStats.MethodOfUpdatingStats[] typeOfChange, float[] valueOfChange)
    {
        OnStatChange?.Invoke(statToChange, typeOfChange, valueOfChange);
    }

    public event Action OnStatUpdate;

    private void StatUpdateTrigger()
    {
        OnStatUpdate?.Invoke();
    }


    public event Action<CharicterStats.StatLog, float> OnTakeDamage;

    public void TakeDamageTrigger(CharicterStats.StatLog statType, float amountOfDamage)
    {
        OnTakeDamage?.Invoke(statType, amountOfDamage);
    }

    public event Action<float> OnHeal;

    public void HealTrigger(float amountOfHealing)
    {
        OnHeal?.Invoke(amountOfHealing);
    }

    public event Action<float> OnHealthBelowZero;

    public void HealthBelowZeroTrigger(float healthAmount)
    {
        Debug.Log("Death");
        OnHealthBelowZero?.Invoke(healthAmount);
    }

}
