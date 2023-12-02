using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public abstract class CharicterDriverBase : NetworkBehaviour
{


    bool isPaused = false;

    private CharacterController charController;

    private CharicterStatsController charicterStats;

    [SerializeField] private GamePlaySettings gPSettings;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            return;
        }

        charController = GetComponent<CharacterController>();
        charicterStats = GetComponent<CharicterStatsController>();

        //Makes it invisable
        Cursor.visible = false;
        //Locks the mouse in place
        Cursor.lockState = CursorLockMode.Locked;

        GameFlowEvents.Current.OnPause += PauseUnPause;

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

    private int[] kInputs;
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (isPaused)
        {
            return;
        }

        kInputs = GameInputs.GetKeyBoardInputs();

        //CharicterMove(kInputs);

        MovementHandeler();
    }

    #region movement
    //************ Initialize Movement Variables ******************************************
    protected float horizontalRotationAngle = 0;

    private float yVector = 0f;



    protected float deltaX;
    protected float deltaZ;
    Vector3 movementDirection;
    Vector3 movement;
    protected virtual void CharicterMove(int[] keyboardInputs)
    {

        movementDirection = DirectectionToMove();

        if ((kInputs[(int)GameInputs.ButtonArrayIndex.Jump] > 0) && IsGrounded())
        {
            Jump();
        }

        if (kInputs[(int)GameInputs.ButtonArrayIndex.Run] > 0)
        {
            movement = CharicterRun(movementDirection);
        }
        else if (kInputs[(int)GameInputs.ButtonArrayIndex.Crouch] > 0)
        {
            movement = CharicterCrouchWalk(movementDirection);
        }
        else //walk
        {
            movement = CharicterWalk(movementDirection);
        }

        movement.y += yVector;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
        CharicterHorizontalRotation();
    }

    protected Vector3 DirectectionToMove()
    {
        //movement direction
        //left right
        deltaX = (kInputs[(int)GameInputs.ButtonArrayIndex.Right] - kInputs[(int)GameInputs.ButtonArrayIndex.Left]);
        //forward back
        deltaZ = (kInputs[(int)GameInputs.ButtonArrayIndex.Forward] - kInputs[(int)GameInputs.ButtonArrayIndex.Backward]);

        return new(deltaX, 0, deltaZ);

    }

    protected virtual void CalculateGravity(float gravity)
    {
        yVector += gravity * Time.deltaTime;
    }

    protected virtual Vector3 CharicterWalk(Vector3 direction)
    {
        if (IsGrounded())
        {
            return CharicerMove(direction, charicterStats.WalkingSpeed.Value);
        }
        else
        {
            CalculateGravity(charicterStats.WalkingFallSpeed.Value);
            return CharicerMove(direction, charicterStats.WalkingSpeedInAir.Value);
        }
    }

    protected virtual Vector3 CharicterRun(Vector3 direction)
    {
        if (IsGrounded())
        {
            return CharicerMove(direction, charicterStats.RunningSpeedMultiplier.Value);
        }
        else
        {
            CalculateGravity(charicterStats.RunningFallSpeed.Value);
            return CharicerMove(direction, charicterStats.RunningFallSpeed.Value);
        }
        
    }

    protected virtual Vector3 CharicterCrouchWalk(Vector3 direction)
    {
        if (IsGrounded())
        {
            return CharicerMove(direction, charicterStats.CrouchWalkingSpeed.Value);
        }
        else
        {
            CalculateGravity(charicterStats.CrouchWalkingFallSpeed.Value);
            return CharicerMove(direction, charicterStats.CrouchWalkingSpeedInAir.Value);
        }
        
    }

    private Vector3 CharicerMove(Vector3 direction, float speed)
    {
        Vector3 movement = new(direction.x * speed, 0, direction.z * speed);

        movement = Vector3.ClampMagnitude(movement, speed);

        movement *= Time.deltaTime;

        return movement;
    }

    protected virtual void CharicterHorizontalRotation()
    {
        horizontalRotationAngle += Input.GetAxis("Mouse X") * gPSettings.MouseSensitivity;
        transform.localEulerAngles = new Vector3(0, horizontalRotationAngle, 0);
    }

    protected virtual void Jump()
    {

        yVector += Mathf.Sqrt(charicterStats.JumpForce.Value * -3.0f * charicterStats.Gravity.Value);

    }
    #endregion

    protected virtual void MovementHandeler()
    {
        if (IsGrounded())
        {
            GroundedCharicterMovement();
        }
        else
        {
            FlyingCharicterMovement();
        }
    }

    protected virtual void GroundedCharicterMovement()
    {
        Vector3 movement;

        if (kInputs[(int)GameInputs.ButtonArrayIndex.Run] > 0)
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value * charicterStats.RunningSpeedMultiplier.Value);
        }
        else if (kInputs[(int)GameInputs.ButtonArrayIndex.Crouch] > 0)
        {
            movement = MoveInputAndCalculation(charicterStats.CrouchWalkingSpeed.Value);
        }
        else
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value);
        }



        // Changes the height position of the player..
        if (kInputs[(int)GameInputs.ButtonArrayIndex.Jump] > 0)
        {
            yVector = 0;
            yVector += Mathf.Sqrt(charicterStats.JumpForce.Value * -3.0f * charicterStats.Gravity.Value);
        }

        movement.y = yVector * Time.deltaTime;



        TransformPlayerMovement(movement, horizontalRotationAngle);
    }

    protected virtual void FlyingCharicterMovement()
    {
        Vector3 movement;

        if (kInputs[(int)GameInputs.ButtonArrayIndex.Run] > 0)
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeedInAir.Value * charicterStats.RunningSpeedMultiplierInAir.Value);
            yVector += charicterStats.RunningFallSpeed.Value * Time.deltaTime;
        }
        else if (kInputs[(int)GameInputs.ButtonArrayIndex.Crouch] > 0)
        {
            movement = MoveInputAndCalculation(charicterStats.CrouchWalkingSpeedInAir.Value);
            yVector += charicterStats.CrouchWalkingFallSpeed.Value * Time.deltaTime;
        }
        else
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeedInAir.Value);
            yVector += charicterStats.WalkingFallSpeed.Value * Time.deltaTime;
        }

        movement.y = yVector * Time.deltaTime;



        TransformPlayerMovement(movement, horizontalRotationAngle);
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
            movement = MoveInputAndCalculation(charicterStats.CrouchWalkingSpeed.Value);
        }
        else
        {
            movement = MoveInputAndCalculation(charicterStats.WalkingSpeed.Value);
        }



        // Changes the height position of the player..
        if (IsGrounded() && (kInputs[(int)GameInputs.ButtonArrayIndex.Jump] > 0))
        {
            yVector += Mathf.Sqrt(charicterStats.JumpForce.Value * -3.0f * charicterStats.Gravity.Value);
        }

        if (!IsGrounded())
        {
            yVector += charicterStats.Gravity.Value * Time.deltaTime;
        }

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


        horizontalRotationAngle += Input.GetAxis("Mouse X") * gPSettings.MouseSensitivity;

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


    #region helperFunctions
    private void PauseUnPause(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    protected virtual bool IsGrounded()
    {
        return charController.isGrounded;
    }

    #endregion
}
