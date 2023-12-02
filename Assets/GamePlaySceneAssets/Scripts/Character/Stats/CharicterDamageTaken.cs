using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterDamageTaken
{

    CharicterStats.StatType typeOfDamageMitigation;

    Stat DamageResistance;
    Stat DamageNegation;
    StatCurrentHealth currentHealth;

    public CharicterDamageTaken(CharicterStats.StatType typeOfMitigation, Stat DamageResistance, Stat DamageNegation, StatCurrentHealth health)
    {
        typeOfDamageMitigation = typeOfMitigation;
        this.DamageResistance = DamageResistance;
        this.DamageNegation = DamageNegation;
        currentHealth = health;
    }


    public void CalculateDamageTaken(CharicterStats.StatType dmgType, float preMitigatedDamage)
    {
        if (dmgType != this.typeOfDamageMitigation)
        {
            return;
        }


        float damageTaken;
        if (preMitigatedDamage < 0)
        {
            preMitigatedDamage *= -1;
        }


        damageTaken = Mathf.Clamp(preMitigatedDamage - DamageNegation.Value, 1, float.MaxValue) * ((Mathf.Clamp(DamageResistance.Value, 1f, 95f) / 100) - 1f);
        Debug.Log(damageTaken);

        currentHealth.DirectUpdate(damageTaken);
    }

}
