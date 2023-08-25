using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCart : MonoBehaviour
{
    public CinemachineDollyCart cinemachineDollyCart;
    

    // Start is called before the first frame update
    void Start()
    {
        cinemachineDollyCart = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void speedON()
    {
        cinemachineDollyCart.m_Speed = 3f;
    }
}