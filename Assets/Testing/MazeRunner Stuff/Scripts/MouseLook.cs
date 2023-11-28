using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;
/*
public class MouseLook : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (!IsLocalPlayer)
        {
            this.gameObject.SetActive(false);
            Destroy(this);
            return;
        }

        //For first person shooters you don't want other objects to hit you and rotate you
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
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


    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        VerticalRotationInputAndCalculation();

        if (!IsHost)
        {

            MoveCameraServerRpc(verticalRotationAngle);
        }
        else
        {
            TransformVerticalRotation(verticalRotationAngle);
        }

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

    [ServerRpc]
    private void MoveCameraServerRpc(float VerticalRotationAngle)
    {
        TransformVerticalRotation(VerticalRotationAngle);
    }


}
*/