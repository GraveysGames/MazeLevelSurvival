using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyStatsController : NetworkBehaviour
{

    static int enemyCount = 0;
    int enemyId;

    //Used to stop enemy getting hit after death droping more items
    bool isDead = false;

    //Base Stats
    [SerializeField] private EnemyStats enemyStats;

    private float currentHealth;

    public override void OnNetworkSpawn()
    {

        PlayerEnemyInteractionEvents.Singleton.OnAttackedObject += TakeDamage;

        if (!IsHost)
        {
            return;
        }

        EnemyStart();
    }

    protected virtual void EnemyStart()
    {
        enemyId = enemyCount;
        enemyCount++;

        currentHealth = enemyStats.StartingHealth;
    }

    /*
    private bool Attack()
    {
        if (!_attackOnCoolDown)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out raycastHit, 20f))
            {
                if (raycastHit.collider.gameObject == _playerFound)
                {
                    StartCoroutine(AttackCoroutine());
                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;

        //attack
        int count = 0;

        Vector3 scale = new(orbScalesUpAmount / 10, orbScalesUpAmount / 10, orbScalesUpAmount / 10);

        while (count < 10)
        {
            transform.localScale += scale;

            count++;
            yield return new WaitForSeconds(attackDuration/10f);
        }

        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        projectile.transform.rotation = transform.rotation;


        StartCoroutine(AttackCoolDownCoroutine(attackCoolDown));

        

        count = 0;

        while (count < 10)
        {
            transform.localScale -= scale;

            count++;
            yield return new WaitForSeconds(attackDuration / 10f);
        }
        _isAttacking = false;
    }

    private IEnumerator AttackCoolDownCoroutine(float attackCoolDown)
    {
        _attackOnCoolDown = true;
        yield return new WaitForSeconds(attackCoolDown);
        _attackOnCoolDown = false;
    }


    public void AgroEnemy(GameObject player)
    {
        _playerFound = player;
    }

    public void DeAgro(GameObject player)
    {
        if (_playerFound == player)
        {
            _playerFound = null;
        }
    }
    */

    private void TakeDamage(GameObject gameObject, float damage)
    {
        if (this.gameObject != gameObject)
        {
            return;
        }


        if (!IsHost)
        {
            TakeDamageServerRPC(damage);
        }
        else
        {
            currentHealth -= (int)damage;

            if (currentHealth <= 0)
            {
                OnDeath();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRPC(float damage)
    {
        currentHealth -= (int)damage;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if (!IsHost)
        {
            return;
        }

        if (!isDead)
        {
            isDead = true;
            Enemy_ItemDrop itemDrop = GetComponent<Enemy_ItemDrop>();
            if (itemDrop != null)
            {
                itemDrop.ProcItemDrop();
            }
            this.gameObject.GetComponent<NetworkObject>().Despawn(true);
            //Destroy(this.gameObject);
        }
    }


    public override void OnDestroy()
    {
        if (!IsHost)
        {
            PlayerEnemyInteractionEvents.Singleton.OnAttackedObject -= TakeDamage;
            return;
        }
        PlayerEnemyInteractionEvents.Singleton.OnAttackedObject -= TakeDamage;
        MazeEvents.Singleton.EnemyDeathTrigger(enemyId);
    }

}
