using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject topLid;
    public GameObject bottomLid;
    void Start()
    {
        topLid = GameObject.Find("EyeLid_Top");
        bottomLid = GameObject.Find("EyeLid_Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //topLid = gameObject.GetComponent<Animation>();
            //topLid.Play("");
            //topLid.GetComponent<Animation>().Play();
            bottomLid.GetComponent<Animation>().Play();
            Debug.Log("Hello");
            //topLid.animation.Play("BlinkTop");
        }
    }
}
