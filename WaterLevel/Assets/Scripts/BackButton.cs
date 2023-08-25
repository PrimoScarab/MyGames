using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public SliderScript sliderScript;

    public GameObject worldScene1;
    public GameObject worldScene2;
    public GameObject worldScene3;

    public GameObject swedenScene1;
    public GameObject swedenScene2;
    public GameObject swedenScene3;

    public GameObject australiaScene1;
    public GameObject australiaScene2;
    public GameObject australiaScene3;

    public GameObject swedenButton;
    public GameObject australiaButton;

    public DisableOrEnableAustralia disableOrEnableAustralia;
    public DisableOrEnableSweden disableOrEnableSweden;

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
        sliderScript.worldMap = true;
        disableOrEnableAustralia.australiaEnabled = false;
        disableOrEnableSweden.swedenEnabled = false;

        if(sliderScript._slider.value >= 2022 && sliderScript._slider.value <= 2049)
        {
            worldScene1.SetActive(true);
            worldScene2.SetActive(false);
            worldScene3.SetActive(false);
            swedenScene1.SetActive(false);
            swedenButton.SetActive(true);
            australiaScene1.SetActive(false);
            australiaButton.SetActive(true);
        }
        if (sliderScript._slider.value >= 2050 && sliderScript._slider.value <= 2084)
        {
            worldScene1.SetActive(false);
            worldScene2.SetActive(true);
            worldScene3.SetActive(false);
            swedenScene2.SetActive(false);
            swedenButton.SetActive(true);
            australiaScene2.SetActive(false);
            australiaButton.SetActive(true);
        }
        if (sliderScript._slider.value >= 2085 && sliderScript._slider.value <= 2100)
        {
            worldScene1.SetActive(false);
            worldScene2.SetActive(false);
            worldScene3.SetActive(true);
            swedenScene3.SetActive(false);
            swedenButton.SetActive(true);
            australiaScene3.SetActive(false);
            australiaButton.SetActive(true);
        }
    }
}
