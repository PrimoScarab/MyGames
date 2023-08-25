using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField]
    Transform[] target;

    [SerializeField]
    protected Transform arrow;

    public float angleSpeedH = 1.0f;
    public float angleSpeedV = 1.0f;
    public int ringNumber = 0;


    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateRotationH();
        UpdateRotationV();

        if (target[ringNumber].gameObject.activeSelf == false)
        {
           
                ringNumber++;
                target[ringNumber].gameObject.SetActive(true);
             
        }
        
        if (ringNumber > 9)
        {
            ringNumber = 9;
            target[10].gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }

    
    private void UpdateRotationH()
    {
        if (target == null || arrow == null)
            return;

        Vector3 toTarget = target[ringNumber].position - arrow.position;

        float dot = Vector3.Dot(arrow.right, toTarget.normalized);
        Quaternion rot = Quaternion.Euler(0f, dot * angleSpeedH * Time.deltaTime, 0f);
        arrow.localRotation *= rot;
    }

    private void UpdateRotationV()
    {
        
        if (target == null || arrow == null)
            return;

        Vector3 toTarget = target[ringNumber].position - arrow.position;

        float dot = Vector3.Dot(arrow.up, toTarget.normalized);
        Quaternion rot = Quaternion.Euler(0f, 0f, -dot * angleSpeedV * Time.deltaTime);
        arrow.localRotation *= rot; 
    }
    
}
