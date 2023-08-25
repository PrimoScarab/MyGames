using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject EyeLid_Top2;
    public GameObject EyeLid_Bottom2;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        blink();

        float forward = Input.GetAxis("Vertical");

        anim.SetFloat("Forward", forward);
    }

    
    void blink()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EyeLid_Top2.GetComponent<Animator>().Play("CloseTop");
            EyeLid_Bottom2.GetComponent<Animator>().Play("CloseBottom");
        }
    }
    
}
