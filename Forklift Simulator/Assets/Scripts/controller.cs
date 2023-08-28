using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive,
    }
    [SerializeField]private driveType drive;

    private inputManager IM;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    private Rigidbody rb;
    public float KPH;
    public float radius = 6f;
    public float motorTorque = 200f;
    public float steeringMax = 90f;

    // Start is called before the first frame update
    void Start()
    {
        getObjects();
    }

    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        steerVehicle();
    }

    void moveVehicle()
    {
        float totalPower;

        if(drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTorque / 4);
            }
        }
        else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTorque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTorque / 2);
            }
        }

        KPH = rb.velocity.magnitude * 3.6f;
            
    }

    private void steerVehicle()
    {
        if(IM.horizontal > 0 )
        {
            wheels[2].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[3].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }
        else if(IM.horizontal < 0)
        {
            wheels[2].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[3].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }
        else
        {
            wheels[2].steerAngle = 0;
            wheels[3].steerAngle = 0;
        }
    }

    void animateWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    void getObjects()
    {
        IM = GetComponent<inputManager>();
        rb = GetComponent<Rigidbody>();
    }
}
