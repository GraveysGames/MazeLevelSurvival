using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityStats : ScriptableObject
{

    public float PhysicalDamageInstant;
    public float PhysicalDamageOverTime;

    public float MagicalDamageInstant;
    public float MagicalDamageOverTime;

    public float CoolDown;

    public float ProjectileSpeed;

}
