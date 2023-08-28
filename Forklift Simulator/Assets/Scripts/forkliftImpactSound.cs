using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forkliftImpactSound : MonoBehaviour
{
    
    public AudioSource[] audioSource;
    public float maxForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;
        float volume = 1;
        if (force <= maxForce)
        {
            volume = force / maxForce;
        }

        if(collision.gameObject.tag == "WoodObject")
        {
            audioSource[0].PlayOneShot(audioSource[0].clip, volume);
        }

        if(collision.gameObject.tag == "MetalObject")
        {
            audioSource[1].PlayOneShot(audioSource[1].clip, volume);
        }
    }
}
