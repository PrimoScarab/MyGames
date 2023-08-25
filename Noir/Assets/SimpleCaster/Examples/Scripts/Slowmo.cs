using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmo : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10.0f)]
    private float timeScale = 1.0f;

    private float lastTimeScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastTimeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastTimeScale != timeScale)
            Time.timeScale = timeScale;

        lastTimeScale = timeScale;
    }
}
