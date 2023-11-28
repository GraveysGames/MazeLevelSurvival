using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class GeneralCharicterDriver : MonoBehaviour
{

    private PlayerStatsController stats;

    private void Start()
    {

        //get stats

        if (GetComponent<PlayerStatsController>())
        {
            Debug.Log("Got Stats");
        }
        

        //end get stats


        charController = GetComponent<CharacterController>();

        movementSpeed = baseMovementSpeed;

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

        GameEvents_SinglePlayer.current.OnTeleportPlayer += TeleportPlayer;

    }

    public void TeleportPlayer(int playerId, Vector3 teleportLocation)
    {
        charController.enabled = false;
        this.transform.position = teleportLocation;
        charController.enabled = true;
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

    public float runningSpeed = 6;

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = MoveInputAndCalculation();

        TransformPlayerMovement(movement, horizontalRotationAngle);

        GameEvents_SinglePlayer.current.PlayerPositionChangedTrigger(transform.position);
    }

    private Vector3 MoveInputAndCalculation()
    {
        //movement direction
        //left right
        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        //forward back
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 movement = new(deltaX, 0, deltaZ);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = Vector3.ClampMagnitude(movement, runningSpeed);
        }
        else
        {
            movement = Vector3.ClampMagnitude(movement, movementSpeed);
        }


        movement.y = gravity;

        movement *= Time.deltaTime;


        horizontalRotationAngle += Input.GetAxis("Mouse X") * sensitivityHorizontal;

        return movement;

    }

    /// <summary>
    /// Move player
    /// </summary>
    /// <param name="Movement"></param>
    /// <param name="HorizontalRotationAngle"></param>
    private void TransformPlayerMovement(Vector3 Movement, float HorizontalRotationAngle)
    {

        Movement = transform.TransformDirection(Movement);
        charController.Move(Movement);
        transform.localEulerAngles = new Vector3(0, HorizontalRotationAngle, 0);

    }

}
*/