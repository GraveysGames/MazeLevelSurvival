using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyAgroController AgroController;
    [SerializeField] GameObject projectilePrefab;

    private float attackRange = 10f;
    private float coolDown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAttackConditions())
        {
            Fire();
        }
    }

    private void Fire()
    {
        Debug.Log("Fire");

        Vector3 playerPosition = AgroController.AgroedPlayer.transform.position;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, new Quaternion());
        projectile.transform.LookAt(playerPosition);
        projectile.GetComponent<EnemyProjectileController>().parent = this.gameObject;
        timeTillNextAttack = Time.time + coolDown;
    }

    private bool CheckAttackConditions()
    {
        bool canFire = true;

        if (AgroController.AgroedPlayer == null)
        {
            canFire = false;
        }
        else if (!IsWithinAttackingDistance())
        {
            //Debug.Log("Not within distance");
            canFire = false;
        }
        else if (!IsNotOnCoolDown())
        {
            //Debug.Log("On cooldown");
            canFire = false;
        }
        else if (!IsLookingAtPlayer())
        {
            //Debug.Log("Not looking at player");
            canFire = false;
        }

        return canFire;
    }


    float timeTillNextAttack = 0;
    private bool IsNotOnCoolDown()
    {

        if (Time.time > timeTillNextAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }


    private bool IsWithinAttackingDistance()
    {

        Vector3 playerPosition = AgroController.AgroedPlayer.transform.position;
        playerPosition.y = 0;
        Vector3 enemyPosition = transform.position;
        enemyPosition.y = 0;

        if (Vector3.Distance(enemyPosition, playerPosition) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float lookingFireGive = 20f;

    private bool IsLookingAtPlayer()
    {
        Vector3 playerPosition = AgroController.AgroedPlayer.transform.position;
        playerPosition.y = 0;
        Vector3 thisEnemyPosition = this.transform.position;
        thisEnemyPosition.y = 0;
        Vector3 directionOfPlayerFromThisEnemy = playerPosition - thisEnemyPosition;
        float angle = Vector3.SignedAngle(this.transform.forward, directionOfPlayerFromThisEnemy, Vector3.up);

        if (angle < lookingFireGive && angle > -lookingFireGive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
