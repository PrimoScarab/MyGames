using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    public static Temperature temperature;

    public float updatedTemperature;
    public Image meter;
    public float maxTemperature;

    // Start is called before the first frame update
    void Start()
    {
        temperature = this;
    }

    // Update is called once per frame
    void Update()
    {
        meter.fillAmount = updatedTemperature / maxTemperature;
    }
}
