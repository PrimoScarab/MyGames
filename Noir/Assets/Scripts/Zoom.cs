using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public GameObject ZoomedInPoster;
    bool zoomed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1") && !zoomed)
        /*
        if (Input.GetButton.sp && !zoomed)
        {
            ZoomedInPoster.SetActive(false);
        }
        */
        if (Input.GetButtonDown("Fire1") && zoomed)
        {
            Debug.Log("FIRE");
            ZoomedInPoster.SetActive(false);
        }
    }

    public void ZoomIn()
    {
        Debug.Log("ZOOM");
        ZoomedInPoster.SetActive(true);
        zoomed = true;
    }
}
