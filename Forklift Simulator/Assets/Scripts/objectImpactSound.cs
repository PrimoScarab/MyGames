using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectImpactSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxForce = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;
        float volume = 1;
        if(force <= maxForce)
        {
            volume = force / maxForce;
        }

        audioSource.PlayOneShot(audioSource.clip, volume);
    }
}
