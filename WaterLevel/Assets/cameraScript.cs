using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField]
    public Sprite frame;
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = frame.bounds.size.x / frame.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = frame.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = frame.bounds.size.y / 2 * differenceInSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
