using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class WheelController : MonoBehaviour
{
    SteeringWheel steeringWheel;
    [SerializeField]
    InputActionReference GearUp, GearDown; 

    public float rbVelocity;

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    

    public LayerMask layerMask;

    GameObject[] wheels;
    private GameObject centerOfMass;
    public GameObject turningPivot;
    public Rigidbody rb;

    [HideInInspector] public int SWcurrentGear; //Gear for steering wheel
    [HideInInspector] public int currentGear = 0;
    [HideInInspector] public int gearDrive = 1;
    [HideInInspector] public int gearNeutral = 0;
    [HideInInspector] public int gearReverse = -1;

    private float accelerationForce;
    private float horizontalInput;
    public float verticalInput;
    public float radius = 6f;
    public float KPH;
    private float maxSpeed = 20f;

    private float breakingForce = 300f;
    private float maxTurnAngle = 70f;

    public float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    public float currentTurnAngle = 0f;
    private float DownForceValue = 50f;

    public TextMeshProUGUI driveText;
    public TextMeshProUGUI neutralText;
    public TextMeshProUGUI reverseText;

    public Image upArrow;
    public Image rightArrow;
    public Image leftArrow;
    public Image rightDarrow;
    public Image leftDarrow;

    public bool neutralGear = true;

    private void Awake()
    {
        steeringWheel = new SteeringWheel();
    }

    private void OnEnable()
    {
        steeringWheel.Gameplay.Enable();
        GearUp.action.performed += PerformGearUp;
        GearDown.action.performed += PerformGearDown;
    }

    

    private void OnDisable()
    {
        steeringWheel.Gameplay.Disable();
        GearUp.action.performed -= PerformGearUp;
        GearDown.action.performed -= PerformGearDown;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheels = GameObject.FindGameObjectsWithTag("Wheel");
        accelerationForce = 1000f;
        centerOfMass = GameObject.Find("mass");
        rb.centerOfMass = centerOfMass.transform.localPosition;
        SWcurrentGear = gearNeutral;
        SWcurrentGear = Mathf.Clamp(SWcurrentGear, gearReverse, gearDrive);
    }

    private void Update()
    {
        //Calls functions
        driving();
        steering();
        gearShift();
        dashBoardArrows();
    }

    private void FixedUpdate()
    {
        addDownForce(); //Calls downforce function
        rbVelocity = rb.velocity.magnitude; 
        KPH = rb.velocity.magnitude * 3.6f; //calculates the velocity in kilometers per hour
    }

    void driving()
    {
        //Keyboard Driving
        verticalInput = Input.GetAxis("Vertical");
        if (currentGear == gearDrive)
        {
            if (verticalInput > 0)
            {
                currentAcceleration = accelerationForce * verticalInput;

                frontRight.motorTorque = currentAcceleration;
                frontLeft.motorTorque = currentAcceleration;
                backRight.motorTorque = currentAcceleration / 2;
                backLeft.motorTorque = currentAcceleration / 2;
            }
            else if (verticalInput == 0)
            {
                frontRight.motorTorque = 0f;
                frontLeft.motorTorque = 0f;
                backRight.motorTorque = 0f;
                backLeft.motorTorque = 0f;
            }

            if (KPH < maxSpeed)
            {
                accelerationForce = 1000f;

            }
            else
            {
                accelerationForce = 0f;
            }
        }
        else if (currentGear == gearNeutral)
        {
            frontRight.motorTorque = 0f;
            frontLeft.motorTorque = 0f;
            backRight.motorTorque = 0f;
            backLeft.motorTorque = 0f;
        }
        else if (currentGear == gearReverse)
        {
            if (verticalInput > 0)
            {
                currentAcceleration = -accelerationForce * verticalInput;

                frontRight.motorTorque = currentAcceleration;
                frontLeft.motorTorque = currentAcceleration;
                backRight.motorTorque = currentAcceleration / 2;
                backLeft.motorTorque = currentAcceleration / 2;
            }
            else if (verticalInput == 0)
            {
                frontRight.motorTorque = 0f;
                frontLeft.motorTorque = 0f;
                backRight.motorTorque = 0f;
                backLeft.motorTorque = 0f;
            }
        }

        if (Input.GetKey("space"))
        {
            currentBreakForce = breakingForce;
        }     
        else
            currentBreakForce = 0f;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        


        //STEERINGWHEEL DRIVING
        /*
        float g = steeringWheel.Gameplay.Gas.ReadValue<float>();
        float b = steeringWheel.Gameplay.Brake.ReadValue<float>();

        if (SWcurrentGear == gearDrive)
        {
            if (g > 0)
            {
                currentAcceleration = accelerationForce * g;

                frontRight.motorTorque = currentAcceleration;
                frontLeft.motorTorque = currentAcceleration;
                backRight.motorTorque = currentAcceleration / 2;
                backLeft.motorTorque = currentAcceleration / 2;
            }
            else if (verticalInput == 0)
            {
                frontRight.motorTorque = 0f;
                frontLeft.motorTorque = 0f;
                backRight.motorTorque = 0f;
                backLeft.motorTorque = 0f;
            }

            if (KPH < maxSpeed)
            {
                accelerationForce = 1000f;

            }
            else
            {
                accelerationForce = 0f;
            }
        }
        else if (SWcurrentGear == gearNeutral)
        {
            frontRight.motorTorque = 0f;
            frontLeft.motorTorque = 0f;
            backRight.motorTorque = 0f;
            backLeft.motorTorque = 0f;
        }
        else if (SWcurrentGear == gearReverse)
        {
            if (g > 0)
            {
                currentAcceleration = -accelerationForce * g;

                frontRight.motorTorque = currentAcceleration;
                frontLeft.motorTorque = currentAcceleration;
                backRight.motorTorque = currentAcceleration / 2;
                backLeft.motorTorque = currentAcceleration / 2;
            }
            else if (g == 0)
            {
                frontRight.motorTorque = 0f;
                frontLeft.motorTorque = 0f;
                backRight.motorTorque = 0f;
                backLeft.motorTorque = 0f;
            }
        }

        if (g < 0 || b > 0)
            currentBreakForce = breakingForce;
        else
            currentBreakForce = 0f;



        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        */
    }

    void steering()
    {
        //Keyboard Steering
        horizontalInput = Input.GetAxis("Horizontal");
        //Sets the current turnangle to be the maxturnangle times the horizontal input
        currentTurnAngle = maxTurnAngle * horizontalInput;

        if (horizontalInput > 0)
        {
            //Sets the frontwheels motortorque to be equal to the current acceleration
            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque = currentAcceleration;

            //Sets backwheels steerangle to the ackerman steering equation times the horizontal input
            backLeft.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * horizontalInput);
            backRight.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * horizontalInput);
        }

        if (horizontalInput < 0)
        {
            //Sets the frontwheels motortorque to be equal to the current acceleration
            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque = currentAcceleration;

            //Sets backwheels steerangle to the ackerman steering equation times the horizontal input
            backLeft.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * horizontalInput);
            backRight.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * horizontalInput);
        }
        

        backLeft.steerAngle = -currentTurnAngle;
        backRight.steerAngle = -currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        


        //SteeringWheelSteering
        /*
        float w = steeringWheel.Gameplay.Steer.ReadValue<float>();

        currentTurnAngle = maxTurnAngle * w;
        
        if (w > 0)
        {   
            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque =  currentAcceleration;
            
            backLeft.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * w);
            backRight.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * w);
        }

        if (w < 0)
        {
            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque = currentAcceleration;
            
            backLeft.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * w);
            backRight.steerAngle = -(Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius * (1.5f / 2))) * w);
        }
        

        backLeft.steerAngle = -currentTurnAngle;
        backRight.steerAngle = -currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        */
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        // Set wheel transform state.
        trans.position = position;
        trans.rotation = rotation;
    }

    void addDownForce()
    {
        //Adds a downforce multiplied with the current velocity
        rb.AddForce(-transform.up * DownForceValue * rb.velocity.magnitude );
    }

    void gearShift()
    {
        /*
        //enables drivetext if SWcurrentGear is equal to gearDrive
        if (SWcurrentGear == gearDrive)
        {
            driveText.enabled = true;
            neutralText.enabled = false;
            reverseText.enabled = false;
        }
        //enables neutralText if SWcurrentGear is equal to gearNeutral
        else if (SWcurrentGear == gearNeutral)
        {
            driveText.enabled = false;
            neutralText.enabled = true;
            reverseText.enabled = false;
        }
        //enables reverseText if current gear is equal to gearReverse
        else if (SWcurrentGear == gearReverse)
        {
            driveText.enabled = false;
            neutralText.enabled = false;
            reverseText.enabled = true;
        }
        */



        //Keyboard
        //Sets currentGear to gearDrive is key M is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentGear = gearDrive;  
        }
        //Sets currentGear to gearNeutral is key N is pressed
        else if (Input.GetKeyDown(KeyCode.N))
        {
            currentGear = gearNeutral;
        }
        //Sets currentGear to gearReverse is key B is pressed
        else if (Input.GetKeyDown(KeyCode.B))
        {
            currentGear = gearReverse;
        }

        //enables drivetext if current gear is equal to gearDrive
        if(currentGear == gearDrive)
        {
            driveText.enabled = true;
            neutralText.enabled = false;
            reverseText.enabled = false;
        }
        //enables neutralText if current gear is equal to gearNeutral
        else if (currentGear == gearNeutral)
        {
            driveText.enabled = false;
            neutralText.enabled = true;
            reverseText.enabled = false;
        }
        //enables reverseText if current gear is equal to gearReverse
        else if (currentGear == gearReverse)
        {
            driveText.enabled = false;
            neutralText.enabled = false;
            reverseText.enabled = true;
        }
        

    }

    //SteeringWheel GearShift Inputs
    private void PerformGearUp(InputAction.CallbackContext obj)
    {
        if (SWcurrentGear == gearNeutral)
        {
            SWcurrentGear = gearDrive;
        }
        if (SWcurrentGear == gearReverse)
        {
            SWcurrentGear = gearNeutral;
        }
    }


    private void PerformGearDown(InputAction.CallbackContext obj)
    {
        if (SWcurrentGear == gearNeutral)
        {
            SWcurrentGear = gearReverse;
        }
        if (SWcurrentGear == gearDrive)
        {
            SWcurrentGear = gearNeutral;
        }
    }


    //Function for directional arrows
    void dashBoardArrows()
    {
        //Activates the up arrow if current turnangle is between 10 and -10 degrees
        if(currentTurnAngle <= 10f && currentTurnAngle >= -10f)
        {
            upArrow.enabled = true;
            rightArrow.enabled = false;
            leftArrow.enabled = false;
            rightDarrow.enabled = false;
            leftDarrow.enabled = false;
        }

        //activates the right arrow if current turnangle is more than 50 degrees
        if (currentTurnAngle >= 50f)
        {
            upArrow.enabled = false;
            rightArrow.enabled = true;
            leftArrow.enabled = false;
            rightDarrow.enabled = false;
            leftDarrow.enabled = false;
        }

        //activates the left arrow if current turnangle is less than -50 degrees
        if ( currentTurnAngle <= -50f)
        {
            upArrow.enabled = false;
            rightArrow.enabled = false;
            leftArrow.enabled = true;
            rightDarrow.enabled = false;
            leftDarrow.enabled = false;
        }

        //activates the right diagonal arrow if current turnangle is between 49 and 11 degrees
        if (currentTurnAngle <= 49f && currentTurnAngle >= 11f)
        {
            upArrow.enabled = false;
            rightArrow.enabled = false;
            leftArrow.enabled = false;
            rightDarrow.enabled = true;
            leftDarrow.enabled = false;
        }

        //activates the left diagonal arrow if current turnangle is between -11 and -49 degrees
        if (currentTurnAngle <= -11f && currentTurnAngle >= -49f)
        {
            upArrow.enabled = false;
            rightArrow.enabled = false;
            leftArrow.enabled = false;
            rightDarrow.enabled = false;
            leftDarrow.enabled = true;
        }
    }
}
