using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform PlayerTransform;

    public Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationSpeed = 5.0f;

    public float heightOffset;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
         heightOffset = -1 * Input.GetAxis("Mouse Y");
    }

    // LateUpdate is called after Update methods
    private void LateUpdate()
    {
        if ((_cameraOffset.y > 10 && heightOffset > 0) || (_cameraOffset.y < 2 && heightOffset < 0))
        {
            heightOffset = 0;
        }

        Vector3 camHeightoffset = new Vector3(0, heightOffset, 0);

        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

            Quaternion camTurnangleY =
                Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationSpeed, Vector3.right);

            _cameraOffset = camTurnAngle * camTurnangleY * _cameraOffset + camHeightoffset;
        }

        

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if(LookAtPlayer || RotateAroundPlayer)
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
