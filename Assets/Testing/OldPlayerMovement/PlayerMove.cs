using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField] private int playerID;

    public Vector3 startingPos = new(-50, 3, -50);


    public override void OnNetworkSpawn()
    {




        charController = GetComponent<CharacterController>();

        if (IsServer)
        {
            charController.Move(new Vector3(0 ,transform.position.y + 5f, 0));
        }

        movementSpeed = baseMovementSpeed;

        if (!IsLocalPlayer)
        {
            //GetComponent<CharacterController>().enabled = false;
            return;
        }

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

    }

    //************ Horizontal **************************************

    private float horizontalRotationAngle = 0;
    //Horizontal Sensitivity Varaible
    public float sensitivityHorizontal = 3.0f;

    //************ Initialize Movement Variables ******************************************
    private CharacterController charController;

    //Gravity or force down
    public float gravity = -9.8f;

    //The walking speed of the charicter
    private float movementSpeed;

    public float baseMovementSpeed = 3.0f;


    // Update is called once per frame
    void Update()
    {

        if (!IsOwner)
        {
            return;
        }

        Vector3 movement = MoveInputAndCalculation();

        if (!IsHost)
        {

            TransformPlayerRotation(horizontalRotationAngle);
            MovePlayerServerRpc(movement, horizontalRotationAngle);

            //GameEvents.current.PlayerPositionTrigger(playerID, transform.position);

        }
        else
        {
            TransformPlayerMovement(movement, horizontalRotationAngle, playerID, transform.position);
        }



    }

    private Vector3 MoveInputAndCalculation()
    {
        //movement direction
        //left right
        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        //forward back
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 movement = new(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed);

        movement.y = gravity;

        movement *= Time.deltaTime;


        horizontalRotationAngle += Input.GetAxis("Mouse X") * sensitivityHorizontal;

        return movement;

    }

    private void TransformPlayerMovement(Vector3 Movement, float HorizontalRotationAngle, int PlayerID, Vector3 PlayerPosition)
    {

        Movement = transform.TransformDirection(Movement);
        charController.Move(Movement);
        transform.localEulerAngles = new Vector3(0, HorizontalRotationAngle, 0);

        //GameEvents.current.PlayerPositionTrigger(PlayerID, PlayerPosition);

    }

    private void TransformPlayerRotation(float HorizontalRotationAngle)
    {
        transform.localEulerAngles = new Vector3(0, HorizontalRotationAngle, 0);
    }

    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 Movement, float HorizontalRotationAngle)
    {
        Movement = transform.TransformDirection(Movement);
        charController.Move(Movement);
        transform.localEulerAngles = new Vector3(0, HorizontalRotationAngle, 0);
    }



}
