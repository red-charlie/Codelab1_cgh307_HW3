using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //this is the like the third way 
    //I've done FPS controllers 
    //trying to find the one 
    //that is the easiest for me.
    public float mouseSensitivity = 100f;
    //the speed in which the turning occurs
    public string MouseXInput;
    //x movement
    public string MouseYInput;
    //y movement
    public Transform playerBody;
    private float xAxisClamp;
    //float for stopping camera movement from flipping us upside down

    private void Awake() {
        //lock the cursor
         Cursor.lockState = CursorLockMode.Locked;
         xAxisClamp = 0.0f;
    }
       void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the x and y values based on the input and sensitivity over time
        float MouseX = Input.GetAxis(MouseXInput) * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis(MouseYInput) * mouseSensitivity * Time.deltaTime;

        //clamp the x axis
        //add it to current mousey
        xAxisClamp += MouseY;

        if(xAxisClamp > 90.0f)
        {
            //don't let it exceed 90 degrees
            xAxisClamp = 90.0f; 
            MouseY = 0.0f;


             // -- clamping with euler angle
            Vector3 eulerRotation = transform.eulerAngles;
            eulerRotation.x = 270.0f;
            //clamping it to 270
            transform.eulerAngles = eulerRotation;
        }
        else if(xAxisClamp < -90.0f)
        {
            //don't let it decrease lower than -90
            xAxisClamp = -90.0f;
            MouseY = 0.0f; 


            // clamping with euler angle
            Vector3 eulerRotation = transform.eulerAngles;
            eulerRotation.x = 90.0f;
            //clamping it to 90
            transform.eulerAngles = eulerRotation;

        }

        //rotate the camera
        transform.Rotate(Vector3.left * MouseY);
        playerBody.Rotate(Vector3.up * MouseX);
    
    }
}
