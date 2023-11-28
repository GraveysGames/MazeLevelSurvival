using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static int enemyCount = 1;
    public int enemyId { get; private set; }
    public int health;
    protected float movementSpeed;

    protected float agroRange;

    protected float attackRange;
    protected float attackCoolDown;
    protected float attackDuration; //time it takes to execute an attack
    protected GameObject projectilePrefab;


    private GameObject projectile;

    protected float stopDistanceFromPlayer;

    [SerializeField] private float orbScalesUpAmount = 0.1f;

    public GameObject _playerFound;

    protected bool isAttacker;
    private bool _isAttacking;
    private bool _attackOnCoolDown;

    protected GameObject currentGameObject;

    public void EnemyStart()
    {
        _isAttacking = false;
        _attackOnCoolDown = false;

        enemyId = enemyCount;
        enemyCount++;

        string enemy_name = name;
        string newName = "";

        foreach (char c in enemy_name)
        {
            if (c == '(')
            {
                break;
            }

            newName += c;
        }

        name = newName + " " + enemyId;

        GameEvents_PlayerEnemyInteraction.current.OnAttackedObject += TakeDamage;
    }

    public void Move()
    {



        if (_playerFound == null)
        {

            transform.Translate(0, 0, movementSpeed * Time.deltaTime);

            float angle = Random.Range(-110, 110);
            transform.Rotate(0, angle, 0);
        }
        else
        {
            float distance = Vector3.Distance(currentGameObject.transform.position, _playerFound.transform.position);
            if (distance <= attackRange)
            {
                if (isAttacker && !_isAttacking)
                {
                    Attack();
                }
            }
            else if(distance >= stopDistanceFromPlayer)
            {
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }

            transform.LookAt(_playerFound.transform);
        }
    }


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


    private void TakeDamage(GameObject gameObject, float damage)
    {
        if (this.gameObject == gameObject)
        {
            health -= (int)damage;
        }
        if (health <= 0)
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        Enemy_ItemDrop itemDrop = GetComponent<Enemy_ItemDrop>();
        if (itemDrop != null)
        {

            Transform itemContainer = transform.parent.parent.Find("Items");

            itemDrop.OnDeath(itemContainer);
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameEvents_PlayerEnemyInteraction.current.OnAttackedObject -= TakeDamage;
        GameEvents_SinglePlayer.current.EnemyDeathTrigger(enemyId);
    }

}
