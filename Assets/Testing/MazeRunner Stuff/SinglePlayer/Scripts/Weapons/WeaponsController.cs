using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private WeaponStats weapon;

    private GameObject currentWeapon;

    void Update()
    {
        if (currentWeapon == null && Input.GetKey(KeyCode.Alpha1))
        {
            currentWeapon = Instantiate(weapon.weaponPrefab, transform);
        }
    }
}
