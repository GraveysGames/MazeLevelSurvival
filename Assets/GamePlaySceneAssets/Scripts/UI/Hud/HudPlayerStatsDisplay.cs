using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class HudPlayerStatsDisplay : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text StatsText;

    Stat[] _stats;

    private void Start()
    {
        _stats = new Stat[CharicterStats.StatLog.GetNames(typeof(CharicterStats.StatLog)).Length];

        StatChangeEvents.Current.OnHudStatChange += UpdateStat;
    }


    private void UpdateStat(Stat statToChange)
    {
        _stats[(int)statToChange.StatType] = statToChange;

        PrintStats();
    }

    private void PrintStats()
    {
        string statsToPrint = "";
        foreach (Stat s in _stats)
        {
            if (s == null)
            {
                return;
            }
            else
            {
                statsToPrint += s.StatName + ": " + s.Value + "\n";
            }
            
        }

        StatsText.text = statsToPrint;

    }


}
