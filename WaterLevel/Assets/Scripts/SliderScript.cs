using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] public Slider _slider;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI tempText;

    public GameObject worldScene1;
    public GameObject worldScene2;
    public GameObject worldScene3;

    public GameObject swedenScene1;
    public GameObject swedenScene2;
    public GameObject swedenScene3;

    public GameObject australiaScene1;
    public GameObject australiaScene2;
    public GameObject australiaScene3;


    public bool worldMap = true;

    public DisableOrEnableSweden disableOrEnableSweden;
    public DisableOrEnableAustralia disableOrEnableAustralia;

    public void onSliderChanged(float value)
    {
        valueText.text = value.ToString();
        
    }

    public void onTempChanged(float temp)
    {
        tempText.text = temp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(worldMap == true)
        {
            if (_slider.value >= 2022 && _slider.value <= 2049)
            {
                worldScene1.SetActive(true);
                worldScene2.SetActive(false);
                worldScene3.SetActive(false);
            }
            else if (_slider.value >= 2050 && _slider.value <= 2084)
            {
                worldScene1.SetActive(false);
                worldScene2.SetActive(true);
                worldScene3.SetActive(false);
            }
            else if (_slider.value >= 2085 && _slider.value <= 2100)
            {
                worldScene1.SetActive(false);
                worldScene2.SetActive(false);
                worldScene3.SetActive(true);
            }
        }
        if(worldMap == false)
        {
            if(disableOrEnableSweden.swedenEnabled == true)
            {
                if (_slider.value >= 2022 && _slider.value <= 2049)
                {
                    swedenScene1.SetActive(true);
                    swedenScene2.SetActive(false);
                    swedenScene3.SetActive(false);
                }
                else if (_slider.value >= 2050 && _slider.value <= 2084)
                {
                    swedenScene1.SetActive(false);
                    swedenScene2.SetActive(true);
                    swedenScene3.SetActive(false);
                }
                else if (_slider.value >= 2085 && _slider.value <= 2100)
                {
                    swedenScene1.SetActive(false);
                    swedenScene2.SetActive(false);
                    swedenScene3.SetActive(true);
                }
            }

            if (disableOrEnableAustralia.australiaEnabled == true)
            {
                if (_slider.value >= 2022 && _slider.value <= 2049)
                {
                    australiaScene1.SetActive(true);
                    australiaScene2.SetActive(false);
                    australiaScene3.SetActive(false);
                }
                else if (_slider.value >= 2050 && _slider.value <= 2084)
                {
                    australiaScene1.SetActive(false);
                    australiaScene2.SetActive(true);
                    australiaScene3.SetActive(false);
                }
                else if (_slider.value >= 2085 && _slider.value <= 2100)
                {
                    australiaScene1.SetActive(false);
                    australiaScene2.SetActive(false);
                    australiaScene3.SetActive(true);
                }
            }
        }
    }
}
