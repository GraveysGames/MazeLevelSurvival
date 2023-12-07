using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform tipOfBarrel;
    [SerializeField] private GameObject bulletPrefab;
    protected void Fire()
    {
        Instantiate(bulletPrefab, tipOfBarrel.position, tipOfBarrel.rotation);
    }
}
