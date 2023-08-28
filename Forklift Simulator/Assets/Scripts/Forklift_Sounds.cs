using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Forklift_Sounds : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    public GameObject myForkLift;
    Fork_Controlls fork_controlls;
    WheelController WC;
    private GameObject fork;
    private Rigidbody carRb;
    public AudioSource[] forkliftSounds;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;
    public float maxForce = 100f;

    bool forksUp;
    bool forksDown;
    bool tiltUp;
    bool tiltDown;
    bool forksLeft;
    bool forksRight;
    bool forksSplit;
    bool forksClamp;


    BoxCollider[] vechileColliders;

    SteeringWheel steeringWheel;

    [SerializeField]
    InputActionReference ForksUp, ForksDown, TiltUp, TiltDown, ForksLeft, ForksRight, ForksSplit, ForksClamp;


    private void Awake()
    {
        steeringWheel = new SteeringWheel();
    }

    private void OnEnable()
    {
        steeringWheel.Enable();
    }

    private void OnDisable()
    {
        steeringWheel.Disable();
    }

    void Start()
    {

        carRb = myForkLift.GetComponent<Rigidbody>();
        fork_controlls = myForkLift.GetComponent<Fork_Controlls>();
        WC = myForkLift.GetComponent<WheelController>();
        vechileColliders = myForkLift.GetComponentsInChildren<BoxCollider>();
    }

    void Update()
    {
        EngineSound();
        ForkUpAndDownSound();
        forkTilt();
        ForkMoveLeftAndRight();
        ForkSpreadAndClamp();
        ReverseSound();
    }

    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude * 3.6f;
        pitchFromCar = (carRb.velocity.magnitude * 3.6f) / 60f;

        if (currentSpeed < minSpeed)
        {
            forkliftSounds[0].pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            forkliftSounds[0].pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            forkliftSounds[0].pitch = maxPitch;
        }
    }


    //Sounds for Joystick input
    /*

    void ForkUpAndDownSound()
    {
       
        forksUp = steeringWheel.Gameplay.ForksUp.ReadValue<float>() > 0;
        forksDown = steeringWheel.Gameplay.ForksDown.ReadValue<float>() > 0;

        if (forksUp)
        {

            if (!forkliftSounds[1].isPlaying && fork_controlls.fork.transform.localPosition.y < fork_controlls.maxY.y)
            {
                forkliftSounds[1].Play();
            }

        }
        else if (!forksUp)
        {
            forkliftSounds[1].Stop();
        }

        if (forksDown)
        {

            if (!forkliftSounds[2].isPlaying && fork_controlls.fork.transform.localPosition.y > fork_controlls.minY.y)
            {
                forkliftSounds[2].Play();
            }

        }
        else if (!forksDown)
        {
            forkliftSounds[2].Stop();
        }

    }
    

    void forkTilt()
    {
        tiltUp = steeringWheel.Gameplay.TiltUp.ReadValue<float>() > 0;
        tiltDown = steeringWheel.Gameplay.TiltDown.ReadValue<float>() > 0;
        

        if (tiltUp)
        {
            if (!forkliftSounds[3].isPlaying && fork_controlls.currentForkRot < fork_controlls.maxRot)
            {
                forkliftSounds[3].Play();
            }
        }
        else if (!tiltUp)
        {
            forkliftSounds[3].Stop();
        }

        if (tiltDown)
        {
            if (!forkliftSounds[4].isPlaying && fork_controlls.currentForkRot > fork_controlls.minRot)
            {
                forkliftSounds[4].Play();
            }
        }
        else if (!tiltDown)
        {
            forkliftSounds[4].Stop();
        }

    }

    void ForkMoveLeftAndRight()
    {
        forksLeft = steeringWheel.Gameplay.ForksLeft.ReadValue<float>() > 0;
        forksRight = steeringWheel.Gameplay.ForksRight.ReadValue<float>() > 0;

        if (forksLeft)
        {
            if (!forkliftSounds[5].isPlaying && fork_controlls.fork.transform.localPosition.x > fork_controlls.forkLpos.transform.localPosition.x)
            {
                forkliftSounds[5].Play();
            }
        }
        else if (!forksLeft)
        {
            forkliftSounds[5].Stop();
        }

        if (forksRight)
        {
            if (!forkliftSounds[6].isPlaying && fork_controlls.fork.transform.localPosition.x < fork_controlls.forkRpos.transform.localPosition.x)
            {
                forkliftSounds[6].Play();
            }
        }
        if (!forksRight)
        {
            forkliftSounds[6].Stop();
        }
    }

    void ForkSpreadAndClamp()
    {
        forksSplit = steeringWheel.Gameplay.ForksSplit.ReadValue<float>() > 0;
        forksClamp = steeringWheel.Gameplay.ForksClamp.ReadValue<float>() > 0;

        if (forksSplit)
        {
            if (!forkliftSounds[9].isPlaying && fork_controlls.forkR.localPosition.x < fork_controlls.forkRMaxLocalPosition.x)
            {
                forkliftSounds[9].Play();
            }
        }
        if (!forksSplit)
        {
            forkliftSounds[9].Stop();
        }

        if (forksClamp)
        {
            if (!forkliftSounds[10].isPlaying && fork_controlls.forkR.transform.localPosition.x > new Vector3(0.00088f, 0, 0).x)
            {
                forkliftSounds[10].Play();

            }
        }
        if (!forksClamp)
        {
            forkliftSounds[10].Stop();
        }

    }
    */
    void ReverseSound()
    {
        if (WC.currentGear == WC.gearReverse)
        {
            forkliftSounds[7].Play();

        }
        else
        {
            forkliftSounds[7].Stop();
        }    
    }
    
    //Sounds for keyboard input

    
    void ForkUpAndDownSound()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {

            if (!forkliftSounds[1].isPlaying && fork_controlls.fork.transform.localPosition.y < fork_controlls.maxY.y)
            {
                forkliftSounds[1].Play();
            }

        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            forkliftSounds[1].Stop();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {

            if (!forkliftSounds[2].isPlaying && fork_controlls.fork.transform.localPosition.y > fork_controlls.minY.y)
            {
                forkliftSounds[2].Play();
            }

        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            forkliftSounds[2].Stop();
        }
    }

    void forkTilt()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (!forkliftSounds[4].isPlaying && fork_controlls.currentForkRot < fork_controlls.maxRot)
            {
                forkliftSounds[4].Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            forkliftSounds[4].Stop();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!forkliftSounds[4].isPlaying && fork_controlls.currentForkRot > fork_controlls.minRot)
            {
                forkliftSounds[4].Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            forkliftSounds[4].Stop();
        }

    }

    void ForkMoveLeftAndRight()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!forkliftSounds[3].isPlaying && fork_controlls.fork.transform.localPosition.x > fork_controlls.forkLpos.transform.localPosition.x)
            {
                forkliftSounds[3].Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            forkliftSounds[3].Stop();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!forkliftSounds[3].isPlaying && fork_controlls.fork.transform.localPosition.x < fork_controlls.forkRpos.transform.localPosition.x)
            {
                forkliftSounds[3].Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            forkliftSounds[3].Stop();
        }
    }

    void ForkSpreadAndClamp()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!forkliftSounds[3].isPlaying && fork_controlls.forkR.localPosition.x < fork_controlls.forkRMaxLocalPosition.x)
            {
                forkliftSounds[3].Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            forkliftSounds[3].Stop();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!forkliftSounds[3].isPlaying && fork_controlls.forkR.transform.localPosition.x > new Vector3(0.00088f, 0, 0).x)
            {
                forkliftSounds[3].Play();

            }
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            forkliftSounds[3].Stop();
        }

    }

    


    
    private void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;
        float volume = 1;
        if (force <= maxForce)
        {
            volume = force / maxForce;
        }

        forkliftSounds[8].PlayOneShot(forkliftSounds[6].clip, volume);
        /*
        foreach (var v in vechile)
        {
            if (collision.gameObject)
            {
                Debug.Log("Collision");
            }
        }
        */
    }
    
}

