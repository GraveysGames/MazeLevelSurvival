using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class CharicterDriverBaseOld : NetworkBehaviour
{

    bool isPaused = false;

    private void Start()
    {

        charController = GetComponent<CharacterController>();

        movementSpeed = baseMovementSpeed;

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

        GameFlowEvents.Current.OnPause += PauseUnPause;

        charicterStats = GetComponent<CharicterStatsController>();
    }

    /*
    private void OnDestroy()
    {
        GameFlowEvents.Current.OnPause -= PauseUnPause;
    }
    */

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

    private CharicterStatsController charicterStats;

    //Gravity or force down
    public float gravity = -9.8f;

    //The walking speed of the charicter
    private float movementSpeed;

    public float baseMovementSpeed;

    public float speedModifierForRunning;

    public float crouchMovementSpeed;

    public float jumpHeight;

    private float yVector = 0f;

    private int[] kInputs;

    // Update is called once per frame
    void Update()
    {

        if (isPaused)
        {
            return;
        }

        //int[] keyBoardInputs = GetKeyboardInputs();

        kInputs = GameInputs.GetKeyBoardInputs();

        PlayerMovement();
    }


    private void PlayerMovement()
    {
        Vector3 movement;

        if (kInputs[(int)GameInputs.ButtonArrayIndex.Run] > 0)
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value * charicterStats.RunningSpeedMultiplier.Value);
        }
        else if (kInputs[(int)GameInputs.ButtonArrayIndex.Crouch] > 0)
        {
            movement = MoveInputAndCalculation(crouchMovementSpeed);
        }
        else
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value);
        }

        if (charController.isGrounded && yVector < 0)
        {
            yVector = 0f;
        }

        // Changes the height position of the player..
        if (charController.isGrounded && (kInputs[(int)GameInputs.ButtonArrayIndex.Jump] > 0))
        {
            yVector += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }


        yVector += gravity * Time.deltaTime;
        movement.y = yVector * Time.deltaTime;


        
        TransformPlayerMovement(movement, horizontalRotationAngle);
    }



    private Vector3 MoveInputAndCalculation(float charicterMovementSpeed)
    {

        //movement direction
        //left right
        float deltaX = (kInputs[(int)GameInputs.ButtonArrayIndex.Right] - kInputs[(int)GameInputs.ButtonArrayIndex.Left]) * charicterMovementSpeed;
        //forward back
        float deltaZ = (kInputs[(int)GameInputs.ButtonArrayIndex.Forward] - kInputs[(int)GameInputs.ButtonArrayIndex.Backward]) * charicterMovementSpeed;


        Vector3 movement = new(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, charicterMovementSpeed);

        //movement.y = gravity;

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



    private void PauseUnPause(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
