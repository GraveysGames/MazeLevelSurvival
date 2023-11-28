using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgroController : MonoBehaviour
{
    [SerializeField] private float agroRange = 20f;

    private LayerMask _enemyLayer;

    private List<Collider> _previousEnemyColliders;

    private void Start()
    {
        _enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        _previousEnemyColliders = new();

        InvokeRepeating("CheckForEnemiesInRange", 5f, 1f);
    }

    private void CheckForEnemiesInRange()
    {
        List<Collider> enemyCollideres = new();
        enemyCollideres.AddRange(Physics.OverlapSphere(this.gameObject.transform.position, agroRange, _enemyLayer));

        foreach (Collider enemyCollider in enemyCollideres)
        {
            //seen collider
            if (_previousEnemyColliders.Contains(enemyCollider))
            {
                //remove from list so whats left is things that left the range
                _previousEnemyColliders.Remove(enemyCollider);
            }
            //new seen collider
            else
            {
                //ping new enemy to agro
                enemyCollider.GetComponent<Enemy>().AgroEnemy(this.gameObject);
            }

        }

        foreach (Collider previousEnemyCollider in _previousEnemyColliders)
        {
            //ping enemy lost agro
            if (previousEnemyCollider != null)
            {
                previousEnemyCollider.GetComponent<Enemy>().DeAgro(this.gameObject);
            }
        }

        _previousEnemyColliders.Clear();
        _previousEnemyColliders.AddRange(enemyCollideres);
    }
}
