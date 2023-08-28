using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Vector3 offset;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WoodObject")
        {
            //other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "WoodObject")
        {
            //other.transform.parent = null;
            //other.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
}
