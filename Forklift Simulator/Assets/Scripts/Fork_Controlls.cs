using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem; //Reference for unitys new input system

public class Fork_Controlls : MonoBehaviour
{
    Input input;
    SteeringWheel steeringWheel;

    //Referenser for inputs in the new input manager
    [SerializeField]
    InputActionReference ForksUp, ForksDown, TiltUp, TiltDown, ForksLeft, ForksRight, ForksSplit, ForksClamp;


    //Positions for different parts of the fork
    public Transform fork; 
    public Transform mast;
    public Transform forks;
    public Transform forkR; //Right fork 
    public Transform forkL; // Left fork
    public Transform forkLl; //Position to the left of left fork
    public Transform forkLr; //Position to the right of the left fork
    public Transform forkRl; //Position to the left of right fork
    public Transform forkRr; //Position to the right of right fork
    public Transform forkRpos; //Position to the right of both forks
    public Transform forkLpos; //Position to the left of both forks

    public float speedTranslate = 0.2f; //Platform travel speed
    public float forkSpeedTranslateX = 0.00000000002f; // Speed for the spread/clamp functions
    public float acceleration = 0.001f; //fork acceleration
    public float forkAccelerationX = 0.0001f;
    public float maxSpeed = 0.5f;
    public float forkMaxSpeedX = 0.005f;
    public float currentForkRot = 0.0f;
    public float rotSpeed = 0.5f;
    public float maxRot = 3f;
    public float minRot = -3f;

    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
    public Vector3 maxYmast; //The maximum height of the mast
    public Vector3 minYmast; //The minimum height of the mast
    public Vector3 maxX; //The maximum X position for the forks
    public Vector3 minX; //The minimum X position for the forks

    //Maximum and minimum positions for the spread/clamp function
    public Vector3 forkLMinLocalPosition;
    public Vector3 forkLMaxLocalPosition;
    public Vector3 forkRMinLocalPosition;
    public Vector3 forkRMaxLocalPosition;

    public float forkDistance; //Distance between the forks
    //The ratio between the forks
    public float forkLRatio = 1.2f; 
    public float forkRRatio = 1.2f;

    


    //Vector3 tp;

    private bool mastMoveTrue = false; //Activate or deactivate the movement of the mast
    public bool spread; //A bool that allows the forks to spread if true

    [SerializeField]
    private Vector3 _sourceVector;
    [SerializeField]
    private Vector3 rotatedVector;
    [SerializeField]
    private Vector3 rotationRange;

    public TMP_Text tiltAngle; //Tilt angle

    public GameObject ForkR; //Right fork
    public GameObject ForkL; //Left fork


    private void Start()
    {
        spread = true; 
    }

    private void Update()
    {   
        tiltAngle.text = currentForkRot.ToString("F0") + "°"; //Sets tiltange text to string with zero decimals + degree signs
    }
        

    void FixedUpdate()
    {

        //Joystick Controlls
        /*
        if (fork.transform.localPosition.y >= maxYmast.y && fork.transform.localPosition.y < maxY.y)
        {
            mastMoveTrue = true;
        }
        else
        {
            mastMoveTrue = false;
        }

        if (fork.transform.localPosition.y <= maxYmast.y)
        {
            mastMoveTrue = false;
        }

        
        if (ForksUp.action.inProgress)
        {
            
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(fork.transform.localPosition.x, maxY.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, maxYmast, speedTranslate * Time.deltaTime);
            }

        }

        else if (ForksDown.action.inProgress)
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(fork.transform.localPosition.x, minY.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, minYmast, speedTranslate * Time.deltaTime);

            }

        }
        else
        {
            speedTranslate = 0.2f;
        }




        
        if (TiltDown.action.inProgress)
        {
            currentForkRot += rotSpeed * Time.deltaTime;
        }

        if(TiltUp.action.inProgress)
        {
            currentForkRot -= rotSpeed * Time.deltaTime;
        }

        if (currentForkRot >= maxRot)
        {
            currentForkRot = maxRot;
        }

        if(currentForkRot <= minRot)
        {
            currentForkRot = minRot;
        }

        fork.transform.localRotation = Quaternion.Euler(currentForkRot, 0, 0);
        

        
        if(ForksLeft.action.inProgress)
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(forkLpos.localPosition.x, fork.transform.localPosition.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);

            

            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
        }

        if (ForksRight.action.inProgress)
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(forkRpos.localPosition.x, fork.transform.localPosition.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            

            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            
        }

        if (spread == true)
        {
            if (ForksSplit.action.inProgress)
            {

                forkLRatio += forkSpeedTranslateX * Time.deltaTime;
                forkLRatio = Mathf.Clamp01(forkLRatio);

                forkRRatio += forkSpeedTranslateX * Time.deltaTime;
                forkRRatio = Mathf.Clamp01(forkRRatio);

            }
        }


        if (ForksClamp.action.inProgress)
        {
            forkLRatio -= forkSpeedTranslateX * Time.deltaTime;
            forkLRatio = Mathf.Clamp01(forkLRatio);

            forkRRatio -= forkSpeedTranslateX * Time.deltaTime;
            forkRRatio = Mathf.Clamp01(forkRRatio);
            spread = true;
        }





        forkL.localPosition = Vector3.Lerp(forkLMinLocalPosition, forkLMaxLocalPosition, forkLRatio);

        forkDistance = Vector3.Distance(forkL.transform.localPosition, forkR.transform.localPosition);

        forkR.localPosition = Vector3.Lerp(forkRMinLocalPosition, forkRMaxLocalPosition, forkRRatio);

        fork.transform.localRotation = Quaternion.Euler(currentForkRot, 0, 0);
        */







        //Keyboard Controlls
        
        
        if (Input.GetKey(KeyCode.Y))
        {
            
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(fork.transform.localPosition.x, maxY.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, maxYmast, speedTranslate * Time.deltaTime);
            }

        }

        else if (Input.GetKey(KeyCode.H))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(fork.transform.localPosition.x, minY.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, minYmast, speedTranslate * Time.deltaTime);

            }

        }
        else
        {
            speedTranslate = 0.2f;
        }
        
        

       
        
        if (Input.GetKey(KeyCode.U))
        {
            currentForkRot += rotSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.J))
        {
            currentForkRot -= rotSpeed * Time.deltaTime;
        }

        if (currentForkRot >= maxRot)
        {
            currentForkRot = maxRot;
        }

        if(currentForkRot <= minRot)
        {
            currentForkRot = minRot;
        }

        fork.transform.localRotation = Quaternion.Euler(currentForkRot, 0, 0);
        

        
        if(Input.GetKey(KeyCode.I))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(forkLpos.localPosition.x, fork.transform.localPosition.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);

            

            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, new Vector3(forkRpos.localPosition.x, fork.transform.localPosition.y, fork.transform.localPosition.z), speedTranslate * Time.deltaTime);
            

            speedTranslate += acceleration;
            if (speedTranslate > maxSpeed)
            {
                speedTranslate = maxSpeed;
            }
            
        }

        
        
        if(spread == true)
        {
            if (Input.GetKey(KeyCode.O))
            {

                forkLRatio += forkSpeedTranslateX * Time.deltaTime;
                forkLRatio = Mathf.Clamp01(forkLRatio);

                forkRRatio += forkSpeedTranslateX * Time.deltaTime;
                forkRRatio = Mathf.Clamp01(forkRRatio);

            }
        }
        
        
            if (Input.GetKey(KeyCode.L))
            {
                forkLRatio -= forkSpeedTranslateX * Time.deltaTime;
                forkLRatio = Mathf.Clamp01(forkLRatio);

                forkRRatio -= forkSpeedTranslateX * Time.deltaTime;
                forkRRatio = Mathf.Clamp01(forkRRatio);
                spread = true;
            }


        forkL.localPosition = Vector3.Lerp(forkLMinLocalPosition, forkLMaxLocalPosition, forkLRatio);

        forkDistance = Vector3.Distance(forkL.transform.localPosition, forkR.transform.localPosition);

        forkR.localPosition = Vector3.Lerp(forkRMinLocalPosition, forkRMaxLocalPosition, forkRRatio);

        fork.transform.localRotation = Quaternion.Euler(currentForkRot, 0, 0); //rotates the fork x axis with the value of currentforkrot
        
        

    }

    //If the children of the fork collides with a gameobject tagged "palletcorner" the spread will stop
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.gameObject.tag == "PalletCorner")
        {
            spread = false;
        }
    }
    
    
}
