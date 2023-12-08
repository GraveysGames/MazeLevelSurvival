using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 6f;
    public float damage = 1.0f;

    private Coroutine destructionCoroutine;

    [SerializeField] private GameObject anchor;

    private LayerMask layerMask;
    private void Start()
    {
        layerMask = LayerMask.GetMask("Everything") - 2^6;
        destructionCoroutine = StartCoroutine(DestructionTimer());
        CheckForRaycastHit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForRaycastHit();
        transform.Translate(Vector3.up * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        CollidedWithSomething(other);
    }

    private void CollidedWithSomething(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PlayerEnemyInteractionEvents.Singleton.AttackedObjectTrigger(other.gameObject, damage);
        }
        else
        {
            Destroy(anchor);
        }
    }


    private void OnDestroy()
    {
        if (destructionCoroutine != null)
        {
            StopCoroutine(destructionCoroutine);
        }
    }

    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(anchor);
    }


    private void CheckForRaycastHit()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.up, out raycastHit, speed, layerMask))
        {
            Debug.Log(raycastHit.transform.name);
            CollidedWithSomething(raycastHit.collider);
        }

    }
}
