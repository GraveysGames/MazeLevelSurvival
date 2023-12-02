using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriverMazeRunnerTest : MonoBehaviour
{

    bool isPaused = false;

    private void Start()
    {
        OnStartKeyBoardInputs();

        charController = GetComponent<CharacterController>();

        movementSpeed = baseMovementSpeed;

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

        GameFlowEvents.Current.OnPause += PauseUnPause;

        charicterStats = GetComponent<CharicterStatsController>();
    }

    private void OnDestroy()
    {
        SettingsEvents.Current.OnGameInputButtonChanged -= GetMovementKeys;
        GameFlowEvents.Current.OnPause -= PauseUnPause;
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

        if (Input.GetKey(runButton))
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value * charicterStats.RunningSpeedMultiplier.Value);
        }
        else if (Input.GetKey(crouchButton))
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
        if (charController.isGrounded && Input.GetKey(jumpButton))
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
        float deltaX = (kInputs[(int)GameInputs.ButtonArrayIndex.Right] - kInputs[(int)GameInputs.ButtonArrayIndex.Left])  * charicterMovementSpeed;
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

    /// <summary>
    /// Handles All Keyboard Input into a nice array for class to use
    /// </summary>
    #region Keyboard Inputs

    private KeyCode forwardButton;
    private KeyCode backwardButton;
    private KeyCode rightwardButton;
    private KeyCode leftwardButton;
    private KeyCode runButton;
    private KeyCode jumpButton;
    private KeyCode crouchButton;

    int enumMovementInputsLength;
    private enum KeyBoardInputIndexes
    {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Run,
        Crouch
    }

    private void OnStartKeyBoardInputs()
    {

        enumMovementInputsLength = KeyBoardInputIndexes.GetNames(typeof(KeyBoardInputIndexes)).Length;

        Debug.Log(enumMovementInputsLength);

        GetMovementKeys();
        SettingsEvents.Current.OnGameInputButtonChanged += GetMovementKeys;

    }

    private void GetMovementKeys()
    {
        forwardButton = GameInputs.ForwardButton;
        backwardButton = GameInputs.BackwardButton;
        rightwardButton = GameInputs.RightButton;
        leftwardButton = GameInputs.LeftButton;
        runButton = GameInputs.RunButton;
        jumpButton = GameInputs.JumpButton;
        crouchButton = GameInputs.CrouchButton;
    }

    private int[] GetKeyboardInputs()
    {
        //forward, backward, left, right, jump, shift, crouch
        int[] inputs = new int[enumMovementInputsLength];

        inputs[(int)KeyBoardInputIndexes.Forward] = ((Input.GetKey(forwardButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Backward] = ((Input.GetKey(backwardButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Left] = ((Input.GetKey(leftwardButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Right] = ((Input.GetKey(rightwardButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Jump] = ((Input.GetKey(jumpButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Run] = ((Input.GetKey(runButton)) ? 1 : 0);
        inputs[(int)KeyBoardInputIndexes.Crouch] = ((Input.GetKey(crouchButton)) ? 1 : 0);


        return inputs;
    }

    #endregion
}
