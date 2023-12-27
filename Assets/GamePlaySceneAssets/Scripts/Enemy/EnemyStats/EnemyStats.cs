using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{

    public string EnemyName;

    //Base Stats

    public float StartingHealth;
    public float MaxHealth;
    public float HealthRegen;
    public float PhysicalResistance;
    public float PhysicalDamageNegation;
    public float MagicResistance;
    public float MagicDamageNegation;

    public float WalkingSpeed;
    public float RunningSpeedMultiplier;
    public float CrouchWalkingSpeed;
    public float JumpForce;

    public float Gravity;

}
