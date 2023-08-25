using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor2 : MonoBehaviour
{

    bool mouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
