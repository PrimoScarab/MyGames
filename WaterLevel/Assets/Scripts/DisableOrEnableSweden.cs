using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOrEnableSweden : MonoBehaviour
{
    //public GameObject worldScene;
    public GameObject worldScene1;
    public GameObject worldScene2;
    public GameObject worldScene3;
    public GameObject swedenScene1;
    public GameObject swedenScene2;
    public GameObject swedenScene3;
    public GameObject swedenButton;
    public GameObject australiaButton;
    public SliderScript sliderScript;

    public bool swedenEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked()
    {
        if(sliderScript.worldMap == true)
        {
            swedenEnabled = true;
            worldScene1.SetActive(false);
            worldScene2.SetActive(false);
            worldScene3.SetActive(false);
            swedenButton.SetActive(false);
            australiaButton.SetActive(false);
            sliderScript.worldMap = false;
        }
    }
}
