using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LookTeleport : MonoBehaviour
{
    private RaycastHit lastRaycastHit;
    public AudioClip audioClip;
    public float range = 1000;
    //public int randomSecond = Random.Range(0, 10);

    //public GameObject BlinkController2;
    Temperature temperature;

    //public GameObject EyeLid_Top2;
    //public GameObject EyeLid_Bottom2;

    
    public int randomTime;
    public float randomNumber;

    public bool CR_Running;
    public bool didFunction = false;
    

    private void Start()
    {
        //BlinkController2.GetComponent<NewerBlink>();
        temperature = GetComponent<Temperature>();

        
                randomNumber = Random.Range(30.0f, 40.0f);
                randomTime = Random.Range(5, 10);
                //StartCoroutine(blink());
        

        //StartCoroutine(TeleporterToLookAt());
    }

    private GameObject GetLookedAtObject()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Camera.main.transform.forward;
        if (Physics.Raycast(origin, direction, out lastRaycastHit, range))
        {
            return lastRaycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void TeleportToLookAt()
    {
        
        transform.position = lastRaycastHit.point + lastRaycastHit.normal * 2f;
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        

    }
    void Update()
    {
        //if (CR_Running)
        //if(Input.GetKeyDown(KeyCode.Q))
        //if(BlinkController2.GetComponent<NewerBlink>().CR_Running == true)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (GetLookedAtObject() != null)
                TeleportToLookAt();
                //TeleporterToLookAt();        
        }      
    }

    /*
    IEnumerator TeleporterToLookAt()
    {
        do
        {
            yield return new WaitForSeconds(5);
            if (GetLookedAtObject() != null)
            {
                transform.position = lastRaycastHit.point + lastRaycastHit.normal * 1.5f;
                if (audioClip != null)
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
               
            
        }
        while (BlinkController2.GetComponent<NewerBlink>().CR_Running == true); 
    }
    */
    /*
    public IEnumerator blink()
    {
        do
        {
            
            EyeLid_Top2.GetComponent<Animator>().Play("CloseTop");
            EyeLid_Bottom2.GetComponent<Animator>().Play("CloseBottom");
            yield return new WaitForSeconds(1);
            CR_Running = true;
            CR_Running = false;
            yield return new WaitForSeconds(randomTime);
            EyeLid_Top2.GetComponent<Animator>().Play("CloseTop");
            EyeLid_Bottom2.GetComponent<Animator>().Play("CloseBottom");
        }
        while (temperature.updatedTemperature > randomNumber);
        CR_Running = false;
    }
    */
}
