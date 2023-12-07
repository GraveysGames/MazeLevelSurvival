using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents_PlayerEnemyInteraction : MonoBehaviour
{

    public static GameEvents_PlayerEnemyInteraction current { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }

    public Action<GameObject, float> OnAttackedObject;

    public void AttackedObjectTrigger(GameObject objectHit, float damage)
    {
        OnAttackedObject?.Invoke(objectHit, damage);
    }
}
