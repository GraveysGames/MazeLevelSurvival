using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemyMove : NetworkBehaviour
{

    //turn radius
    float turnSpeed = 5f;
    //movement speed
    float movementSpeed = 1f;

    //perfered Height
    float nominalHeight = 4f;

    //how fast it moves up and down
    float heightMoveSpeed = 0.1f;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (!IsHost)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsHost)
        {
            return;
        }

        MoveWanderAI();

        /*
        if (_playerFound == null)
        {

            transform.Translate(0, 0, movementSpeed * Time.deltaTime);

            float angle = Random.Range(-5, 5);
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
            else if (distance >= stopDistanceFromPlayer)
            {
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }

            transform.LookAt(_playerFound.transform);
        }
        */
    }

    private void MoveWanderAI()
    {

        WanderHorizontalRotation();
        WanderMove();
    }

    private float _rayCastDistnace = 2f;
    private void WanderHorizontalRotation()
    {
        float angle;

        RaycastHit ray;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out ray, _rayCastDistnace, LayerMask.GetMask("Ground")))
        {
            angle = Vector3.Angle(this.transform.forward, ray.normal);
            angle = Mathf.Clamp(angle, -turnSpeed, turnSpeed);
        }
        else
        {
           angle  = Random.Range(-turnSpeed, turnSpeed);
        }



        transform.Rotate(0, angle, 0);
    }
     private void WanderMove()
    {
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
    }

}
