using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Stat
{
    public string StatName { protected set; get; }
    protected string discriptionOfStat;
    public CharicterStats.StatLog StatType { protected set; get; }
    protected float baseStatValue;
    protected float baseStatMultiplier;
    protected float baseStatAddative;
    protected float totalAddative;
    protected float totalMultiplier;
    public float Value { protected set; get; }

    public bool HasUpdate { protected set; get; }

    public Stat(CharicterStats.StatLog typeOfStat, float baseStat)
    {
        StatName = typeOfStat.ToString();
        StatType = typeOfStat;
        baseStatValue = baseStat;
        baseStatMultiplier = 1;
        baseStatAddative = 0;
        totalAddative = 0;
        totalMultiplier = 1;
        Value = baseStat;
        HasUpdate = false;

        StatChangeEvents.Current.OnStatChange += this.UpdateStat;
        //StatChangeEvents.Current.OnStatChangeOverTime += this.OverTime;
    }

    public Stat(CharicterStats.StatLog typeOfStat, float baseStat, bool hasUpdate)
    {
        StatName = typeOfStat.ToString();
        StatType = typeOfStat;
        baseStatValue = baseStat;
        baseStatMultiplier = 1;
        baseStatAddative = 0;
        totalAddative = 0;
        totalMultiplier = 1;
        Value = baseStat;
        HasUpdate = hasUpdate;

        StatChangeEvents.Current.OnStatChange += this.UpdateStat;

        if (hasUpdate)
        {
            StatChangeEvents.Current.OnStatUpdate += this.Update;
        }

        //
    }

    protected virtual void Update()
    {

    }

    protected virtual void UpdateStat(CharicterStats.StatLog[] typeOfStat, CharicterStats.MethodOfUpdatingStats[] typeOfChange, float[] valueOfStatChange)
    {
        for (int i = 0; i < valueOfStatChange.Length; i++)
        {
            if (typeOfStat[i] == StatType)
            {
                switch (typeOfChange[i])
                {
                    case CharicterStats.MethodOfUpdatingStats.BaseAdd:
                        baseStatAddative += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.BaseMultiply:
                        baseStatMultiplier += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.TotalAdd:
                        totalAddative += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.TotalMultiply:
                        totalMultiplier += valueOfStatChange[i];
                        break;
                }
                RecalculateTotalStat();
            }
        }

    }
    protected virtual void RecalculateTotalStat()
    {
        Value = totalMultiplier * ((baseStatMultiplier * (baseStatValue + baseStatAddative)) + totalAddative);
        StatChangeEvents.Current.HudStatChangeTrigger(this);
        Print();
    }

    public void Print()
    {
        Debug.Log(StatName + " Value: " + Value + "\n\t"
                    + "statType: " + StatType + "\n\t"
                    + "baseStatValue: " + baseStatValue + "\n\t"
                    + "baseStatMultiplier: " + baseStatMultiplier + "\n\t"
                    + "baseStatAddative: " + baseStatAddative + "\n\t"
                    + "totalAddative: " + totalAddative + "\n\t"
                    + "totalMultiplier: " + totalMultiplier + "\n\t"
                    );
    }

    /*
    private void OverTime(StatTypes[] typeOfStat, MethodOfUpdatingStats[] typeOfChange, float[] valueOfStatChange, float[] amountOfTime)
    {
        for (int i = 0; i < valueOfStatChange.Length; i++)
        {
            if (typeOfStat[i] == statType)
            {
                break;
            }
            else
            {
                OverTimeFunctionality = StatChangeEvents.Current.StartCoroutine(OverTimeCoroutine(this, valueOfStatChange, amountOfTime));
            }
        }
    }
    private IEnumerator OverTimeCoroutine(Stat statHealthRegen, float valueOfStatChange, float amountOfTime)
    {

        return null;
    }

    private IEnumerator ReturnStat(float amountOfTime, float initialValue)
    {
        yield return new WaitForSeconds(amountOfTime);


        yield return initialValue;
    }
    */

}

public class StatBasic : Stat
{
    public StatBasic(CharicterStats.StatLog typeOfStat, float baseStat) : base(typeOfStat, baseStat)
    {
        RecalculateTotalStat();
    }
}

public class StatCurrentHealth : Stat
{
    private Stat ContingentStat;
    public StatCurrentHealth(CharicterStats.StatLog typeOfStat, float baseStat, Stat ContingentStat) : base(typeOfStat, baseStat)
    {
        this.ContingentStat = ContingentStat;

        StatChangeEvents.Current.OnHeal += DirectUpdate;

        RecalculateTotalStat();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeOfStat"></param>
    /// <param name="typeOfChange">Doesn't matter will just add for all types</param>
    /// <param name="valueOfStatChange">In per second</param>
    protected override void UpdateStat(CharicterStats.StatLog[] typeOfStat, CharicterStats.MethodOfUpdatingStats[] typeOfChange, float[] valueOfStatChange)
    {

        for (int i = 0; i < valueOfStatChange.Length; i++)
        {
            if (typeOfStat[i] == StatType)
            {
                switch (typeOfChange[i])
                {
                    case CharicterStats.MethodOfUpdatingStats.BaseAdd:
                        Value += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.BaseMultiply:
                        Value += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.TotalAdd:
                        Value += valueOfStatChange[i];
                        break;
                    case CharicterStats.MethodOfUpdatingStats.TotalMultiply:
                        Value += valueOfStatChange[i];
                        break;
                    default:
                        Value += valueOfStatChange[i];
                        break;
                }
                RecalculateTotalStat();
            }
        }

    }

    public void DirectUpdate(float valueOfStatChange)
    {
        UpdateStat(new CharicterStats.StatLog[] { CharicterStats.StatLog.currentHealth }, new CharicterStats.MethodOfUpdatingStats[] { CharicterStats.MethodOfUpdatingStats.TotalAdd }, new float[] { valueOfStatChange });
    }

    protected override void RecalculateTotalStat()
    {
        Value = Mathf.Clamp(Value, float.MinValue, ContingentStat.Value);
        if (Value < 0)
        {
            StatChangeEvents.Current.HealthBelowZeroTrigger(Value);
        }
        StatChangeEvents.Current.HudStatChangeTrigger(this);
    }
}

public class StatHealthRegen : Stat
{
    private StatCurrentHealth ContingentStat;
    public StatHealthRegen(CharicterStats.StatLog typeOfStat, float baseStat, StatCurrentHealth ContingentStat) : base(typeOfStat, baseStat, true)
    {
        this.ContingentStat = ContingentStat;

        RecalculateTotalStat();
    }

    protected override void Update()
    {
        ContingentStat.DirectUpdate(Value);
    }
}


