using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    bool isPaused;

    private KeyCode runButton;

    // Start is called before the first frame update
    void Start()
    {
        //For first person shooters you don't want other objects to hit you and rotate you
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }



        GameFlowEvents.Current.OnPause += PauseUnPause;
        SettingsEvents.Current.OnGameInputButtonChanged += SetUpButtons;

        SetUpButtons();

        fieldOfView = Camera.main.fieldOfView;
        Debug.Log("Field of View " + fieldOfView);

    }


    private void SetUpButtons()
    {
        runButton = GameInputs.RunButton;
    }

    //************* Initialize Rotational Variables ***************************************

    //creates an easy way to keep track of the axis of rotation and change them 
    public enum RotationalAxis
    {
        mouseXandY = 0,
        mouseX = 1,
        mouseY = 2
    }

    //creates a variable to hold the current axis of rotation
    public RotationalAxis axis = RotationalAxis.mouseXandY;

    //Horizonatal Rotation Varaibles


    //Keeps track if the modified view angle


    //Virtical Rotation Varaibles

    //Vertical Sensitivity Varaible
    public float sensitivityVertical = 3.0f;

    //Limits the amount the player can look up or down
    public float minVerticalLookAngle = -75.0f;
    public float maxVerticalLookAngle = 75.0f;


    //Keeps track if the modified view angle
    private float verticalRotationAngle = 0;


    //Field Of View Attributes

    public float fieldOfView;

    public float maxRunFieldOfView;

    public float runFieldOfViewChangeOverTime;

    public float fieldOfViewDegradeRate;

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }


        CameraMovement();


        RunningFieldOfViewChange();
    }


    private void CameraMovement()
    {
        VerticalRotationInputAndCalculation();
        TransformVerticalRotation(verticalRotationAngle);
    }

    private void VerticalRotationInputAndCalculation()
    {
        // rotate( x, y, z) also rotate( pitch, yaw, roll)  
        //transform.Rotate(0, rotationSpeed, 0);

        //Gets the current change in looking angle from the mouse
        verticalRotationAngle -= Input.GetAxis("Mouse Y") * sensitivityVertical;


        //clamps the value between the max and min looking angle then sets that as the new rotation angle 
        verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, minVerticalLookAngle, maxVerticalLookAngle);

    }

    private void TransformVerticalRotation(float VerticalRotationAngle)
    {
        //transforms the current player eularangles to the new directional vector
        transform.localEulerAngles = new Vector3(VerticalRotationAngle, 0, 0);
    }


    private void RunningFieldOfViewChange()
    {
        float currentFieldOfView = Camera.main.fieldOfView;
        if (Input.GetKey(runButton))
        {
            currentFieldOfView += runFieldOfViewChangeOverTime;
            currentFieldOfView = Mathf.Clamp(currentFieldOfView, fieldOfView, maxRunFieldOfView);
            Camera.main.fieldOfView = currentFieldOfView;
        }
        else
        {
            currentFieldOfView -= fieldOfViewDegradeRate;
            currentFieldOfView = Mathf.Clamp(currentFieldOfView, fieldOfView, maxRunFieldOfView);
            Camera.main.fieldOfView = currentFieldOfView;
        }

        
    }


    private void PauseUnPause(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
