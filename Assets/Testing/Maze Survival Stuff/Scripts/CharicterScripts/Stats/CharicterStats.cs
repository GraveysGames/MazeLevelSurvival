using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// All of the stats on a charicter assuming dmg stats arent charicter bound
/// To add a new stat must be added in base enum and CharicterController
/// </summary>
[CreateAssetMenu]
public class CharicterStats : ScriptableObject
{
    public string charicterName { private set; get; }

    //Base Stats

    public float baseStartingHealth;
    public float baseMaxHealth;
    public float baseHealthRegen;
    public float basePhysicalResistance;
    public float basePhysicalDamageNegation;
    public float baseMagicResistance;
    public float baseMagicDamageNegation;

    public float baseWalkingSpeed;
    public float baseRunningSpeed;

    public float baseJumpForce;
    public float baseWalkingSpeedInAir;
    public float baseRunningSpeedInAir;
    public float baseWalkingFallSpeed;
    public float baseRunningFallSpeed;


    //Reference to which base stat is being affected 
    public enum StatLog
    {
        currentHealth,
        maxHealth,
        healthRegen,
        physicalResistance,
        physicalNegation,
        magicResistance,
        magicNegation,
        walkingSpeed,
        runningSpeedMultiplier,
        jumpHeight,
        walkingSpeedInAir,
        runningSpeedInAir,
        walkingFallSpeed,
        runningFallSpeed
    }

    //how the base stat is being affected
    public enum MethodOfUpdatingStats
    {
        BaseAdd,
        BaseMultiply,
        TotalAdd,
        TotalMultiply,
        overTime
    }

    public enum StatType
    {
        Health,
        PhysicalDamageMitigation,
        MagicalDamageMitigation,
        Movement
    }

}
