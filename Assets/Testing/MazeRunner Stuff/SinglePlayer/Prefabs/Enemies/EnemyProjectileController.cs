using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 1.0f;

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, 0, speed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEvents_PlayerEnemyInteraction.current.AttackedObjectTrigger(other.gameObject, damage);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
