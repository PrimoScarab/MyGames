using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MetalObject")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "MetalObject")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
