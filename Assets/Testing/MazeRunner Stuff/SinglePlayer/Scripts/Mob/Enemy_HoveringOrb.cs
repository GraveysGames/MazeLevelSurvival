using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HoveringOrb : Enemy
{

    private int _health = 10;
    private float _movementSpeed = 5f;
    private float _agroRange = 20f;

    private float _attackRange = 8f;
    private float _attackDuration = 1f;
    private float _attackCoolDown = 5f;

    [SerializeField] private GameObject _projectilePrefab;

    private float _stopDistanceFromPlayer = 3f;

    private bool _isAttacker = true;

    // Start is called before the first frame update
    void Start()
    {
        health = _health;
        movementSpeed = _movementSpeed;
        agroRange = _agroRange;
        attackRange = _attackRange;
        attackCoolDown = _attackCoolDown;
        isAttacker = _isAttacker;
        currentGameObject = this.gameObject;
        attackDuration = _attackDuration;
        projectilePrefab = _projectilePrefab;
        stopDistanceFromPlayer = _stopDistanceFromPlayer;
        EnemyStart();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
