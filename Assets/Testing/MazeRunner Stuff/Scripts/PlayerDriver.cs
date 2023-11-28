using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;
/*
public class PlayerDriverMazeRunnerTest //: NetworkBehaviour
{
    /*
    [SerializeField] private int playerID;

    public Vector3 startingPos = new(-50, 3, -50);

    private void Start()
    {

        if (!IsLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = false;
            playerID = (int)DateTime.Now.Ticks;
            return;
        }
        playerID = (int)DateTime.Now.Ticks;
        GameEvents.current.OnPlayerPositionAvailableTrigger += OnPlayerPositionAvailable;
        OnPlayerReadyForPosition();

        charController = GetComponent<CharacterController>();

        TelportPlayer(startingPos);

        movementSpeed = baseMovementSpeed;

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void OnPlayerConnected()
    {
        
    }




    /// <summary>
    /// listens for player start position in the maze
    /// </summary>
    /// <param name="playerPosition"></param>
    private void OnPlayerPositionAvailable(int playerID, Vector3 playerPosition)
    {
        if (this.playerID == playerID)
        {

            TelportPlayer(playerPosition);

            GameEvents.current.OnPlayerPositionAvailableTrigger -= OnPlayerPositionAvailable;
        }

    }

    private void TelportPlayer(Vector3 playerPosition)
    {
        GetComponent<CharacterController>().enabled = false;

        this.startingPos = playerPosition;
        this.transform.position = startingPos;

        GetComponent<CharacterController>().enabled = true;
    }

    /// <summary>
    /// Broadcasts its ready to get start position
    /// </summary>
    private void OnPlayerReadyForPosition()
    {
        GameEvents.current.PlayerReadyForPositionTrigger(playerID);
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

        if (!IsLocalPlayer)
        {
            return;
        }

        Vector3 movement = MoveInputAndCalculation();

        if (!IsHost)
        {

            MovePlayerServerRpc(movement, horizontalRotationAngle, playerID, transform.position);

            GameEvents.current.PlayerPositionTrigger(playerID, transform.position);

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

        GameEvents.current.PlayerPositionTrigger(PlayerID, PlayerPosition);

    }

    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 Movement, float HorizontalRotationAngle, int PlayerID, Vector3 PlayerPosition)
    {
        TransformPlayerMovement(Movement, HorizontalRotationAngle, PlayerID, PlayerPosition);
    }


   

}
*/