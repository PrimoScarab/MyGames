using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlink : MonoBehaviour
{
    public GameObject EyeLid_Top;
    public GameObject EyeLid_Bottom;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EyeLid_Top.GetComponent<Animator>().Play("BlinkTop");
            EyeLid_Bottom.GetComponent<Animator>().Play("BlinkBottom");
        }
    }
}
