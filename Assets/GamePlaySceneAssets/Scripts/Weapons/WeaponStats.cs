using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;
    public GameObject weaponPrefab;
}
