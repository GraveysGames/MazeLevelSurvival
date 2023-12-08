using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerEnemyInteractionEvents.Singleton.OnAttackedObject += TakeDamage;
    }

    private void TakeDamage(GameObject gameObject, float damage)
    {
        if (this.gameObject == gameObject)
        {
            //GetComponent<CharicterStatsController>().TakeDamage((int)damage);
        }
    }

}
