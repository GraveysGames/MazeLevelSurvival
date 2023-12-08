using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteractionEvents : MonoBehaviour
{

    public static PlayerEnemyInteractionEvents Singleton { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Singleton = this;
    }

    public Action<GameObject, float> OnAttackedObject;

    public void AttackedObjectTrigger(GameObject objectHit, float damage)
    {
        OnAttackedObject?.Invoke(objectHit, damage);
    }
}
