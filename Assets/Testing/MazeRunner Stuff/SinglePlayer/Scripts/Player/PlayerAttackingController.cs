using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private GameObject bullet;
    public float bulletSpeed = 5f;

    private Ray lastBulletShot;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask("Enemy")))
            {
                if (raycastHit.collider != null)
                {
                    GameEvents_PlayerEnemyInteraction.current.AttackedObjectTrigger(raycastHit.collider.gameObject, 1f);
                }
            }
        }
        */
    }

}
