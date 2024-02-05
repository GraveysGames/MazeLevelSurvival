using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 1.0f;

    public GameObject parent;

    private Coroutine destructionCoroutine;

    private void Awake()
    {
        destructionCoroutine = StartCoroutine(DestructionTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerEnemyInteractionEvents.Singleton.AttackedObjectTrigger(other.gameObject, damage);
        }
        else if (other.gameObject == parent)
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void OnDestroy()
    {
        if (destructionCoroutine != null)
        {
            StopCoroutine(destructionCoroutine);
        }
    }

    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(5f);
        DestroyProjectile();
    }


    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
