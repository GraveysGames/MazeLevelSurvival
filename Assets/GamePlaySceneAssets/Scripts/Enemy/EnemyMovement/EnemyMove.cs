using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemyMove : NetworkBehaviour
{
    static int count = 0;
    public int id;

    //turn radius
    float turnSpeed = 180f;
    //movement speed
    float movementSpeed = 1f;

    //perfered Height
    float nominalHeight = 4f;

    //how fast it moves up and down
    float heightMoveSpeed = 0.5f;

    [SerializeField] EnemyAgroController AgroController;

    HorizontalRotationAI rotateWander;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (!IsHost)
        {
            return;
        }

        if (heightWander == null)
        {
            heightWander = new HoverWanderAI();
        }

        rotateWander = new HorizontalRotationAI();

        id = count;
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsHost)
        {
            return;
        }

        if (!Physics.Raycast(transform.position, Vector3.down, 10f, LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (AgroController.AgroedPlayer == null)
        {
            MoveWanderAI();
        }
        else
        {
            AttackMovement();
        }

    }

    private float attackRange = 10f;
    private float stopDistanceFromPlayer = 5f;

    private void AttackMovement()
    {
        float distance = Vector3.Distance(transform.position, AgroController.AgroedPlayer.transform.position);
        if (distance <= attackRange)
        {
            AttackHorizontalRotation();
            Vector3 wanderUpDown = WanderUpDown(transform.position);
            transform.Translate(0, wanderUpDown.y, 0);
            //attack
        }
        else if (distance >= stopDistanceFromPlayer)
        {
            AttackHorizontalRotation();
            Vector3 wanderUpDown = WanderUpDown(transform.position);
            transform.Translate(0, wanderUpDown.y, movementSpeed * Time.deltaTime);
        }
    }

    private void AttackHorizontalRotation()
    {
        float angle = rotateWander.GetAttackRotation(this.transform, AgroController.AgroedPlayer.transform);
        transform.Rotate(0, angle, 0);
    }


    private void MoveWanderAI()
    {
        Vector3 moveVector = new();

        WanderHorizontalRotation();
        moveVector = WanderUpDown(moveVector);
        WanderMove(moveVector);
    }

    HoverWanderAI heightWander;

    private Vector3 WanderUpDown(Vector3 moveVector)
    {
        moveVector.y = heightWander.HeightWander(transform.position.y);
        return moveVector;
    }

    private void WanderHorizontalRotation()
    {
        float angle = rotateWander.GetWanderRotation(id, this.transform);
        transform.Rotate(0, angle, 0);
    }
     private void WanderMove(Vector3 moveVector)
    {
        moveVector.z = movementSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

}

//wanders up and down
class HoverWanderAI
{
    private float maxHeight = 8f;
    private float minHeight = 1f;
    private float heightMovementSpeed = 0.5f;

    private float destination;
    private float destinationGive = 0.01f;
    public HoverWanderAI()
    {
        destination = UnityEngine.Random.Range(minHeight, maxHeight);
    }

    public float HeightWander(float currentHeight)
    {
        float movement;

        float checkHowCloseToDestination = destination - currentHeight;
        if (checkHowCloseToDestination < destinationGive && checkHowCloseToDestination > -destinationGive)
        {
            //new destination
            destination = UnityEngine.Random.Range(minHeight, maxHeight);
        }
        float direction = destination - currentHeight;
        if (direction > 0)
        {
            movement = heightMovementSpeed * Time.deltaTime;
        }
        else
        {
            movement = -heightMovementSpeed * Time.deltaTime;
        }

        return movement;
    }

}

class HorizontalRotationAI
{
    float rotationSpeed = 100f;
    GameObject lastWallHit;
    bool flagDestinationPlayer = false;

    float destination;
    float destinationGive = 3f;

    float _rayCastDistnace = 2f;

    public HorizontalRotationAI()
    {
        destination = UnityEngine.Random.Range(-180f, 180f);
    }

    public float GetWanderRotation(int id, Transform thisObjectsTransform)
    {
        float rotation;

        //Debug.Log(id + " " + destination);

        CheckForWalls(thisObjectsTransform);

        if (destination < destinationGive && destination > -destinationGive)
        {
            destination = UnityEngine.Random.Range(-180f, 180f);
            //Debug.Log(id + " Acheived Destination");
        }

        rotation = Mathf.Clamp(destination, -rotationSpeed, rotationSpeed);

        rotation *= Time.deltaTime;

        destination -= rotation;

        return rotation;
    }

    public float GetAttackRotation(Transform thisObjectsTransform, Transform agroedPlayerTransform)
    {
        float rotation;

        CheckForWalls(thisObjectsTransform);

        if (lastWallHit == null)
        {
            if (!flagDestinationPlayer)
            {

                Vector3 playerPosition = agroedPlayerTransform.position;
                //playerPosition.y = 0;
                Vector3 thisEnemyPosition = thisObjectsTransform.position;
                //thisEnemyPosition.y = 0;

                Vector3 directionOfPlayerFromThisEnemy = playerPosition - thisEnemyPosition;
                Debug.DrawRay(thisObjectsTransform.position, directionOfPlayerFromThisEnemy, Color.red);
                directionOfPlayerFromThisEnemy = directionOfPlayerFromThisEnemy.normalized;

                destination = Vector3.SignedAngle(thisObjectsTransform.forward, directionOfPlayerFromThisEnemy, Vector3.up);

                Debug.DrawRay(thisObjectsTransform.position, thisObjectsTransform.forward, Color.blue);
            }
        }

        if (destination < destinationGive && destination > -destinationGive)
        {
            return 0;
        }

        rotation = Mathf.Clamp(destination, -rotationSpeed, rotationSpeed);

        rotation *= Time.deltaTime;

        destination -= rotation;

        return rotation;
    }

    private void CheckForWalls(Transform thisObjectsTransform)
    {
        RaycastHit ray;

        if (Physics.Raycast(thisObjectsTransform.position, thisObjectsTransform.forward, out ray, _rayCastDistnace, LayerMask.GetMask("Ground")))
        {
            if (ray.collider.gameObject != lastWallHit)
            {
                destination = Vector3.SignedAngle(thisObjectsTransform.forward, ray.normal, Vector3.up);
                lastWallHit = ray.collider.gameObject;
                flagDestinationPlayer = false;
            }
        }
        else
        {
            lastWallHit = null;
        }
    }

}