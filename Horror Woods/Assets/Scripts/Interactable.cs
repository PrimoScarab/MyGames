using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public Sprite interactIcon;
    public int ID;
    public Vector2 iconSize;


    public GameObject[] papers1;
    public GameObject[] papers2;
    public GameObject[] papers3;
    public GameObject[] papers4;
    public GameObject[] papers5;
    public GameObject[] papers6;
    public GameObject[] papers7;
    public GameObject[] papers8;

    // Start is called before the first frame update
    void Start()
    {
        ID = Random.Range(0, 999999);

        papers1 = GameObject.FindGameObjectsWithTag("paper1");
        papers2 = GameObject.FindGameObjectsWithTag("paper2");
        papers3 = GameObject.FindGameObjectsWithTag("paper3");
        papers4 = GameObject.FindGameObjectsWithTag("paper4");
        papers5 = GameObject.FindGameObjectsWithTag("paper5");
        papers6 = GameObject.FindGameObjectsWithTag("paper6");
        papers7 = GameObject.FindGameObjectsWithTag("paper7");
        papers8 = GameObject.FindGameObjectsWithTag("paper8");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyP1()
    {
        foreach(var paper1 in papers1)
        {
            paper1.SetActive(false);
            
        }
    }

    public void destroyP2()
    {
        foreach (var paper2 in papers2)
        {
            paper2.SetActive(false);

        }
    }

    public void destroyP3()
    {
        foreach (var paper3 in papers3)
        {
            paper3.SetActive(false);

        }
    }

    public void destroyP4()
    {
        foreach (var paper4 in papers4)
        {
            paper4.SetActive(false);

        }
    }

    public void destroyP5()
    {
        foreach (var paper5 in papers5)
        {
            paper5.SetActive(false);

        }
    }

    public void destroyP6()
    {
        foreach (var paper6 in papers6)
        {
            paper6.SetActive(false);

        }
    }

    public void destroyP7()
    {
        foreach (var paper7 in papers7)
        {
            paper7.SetActive(false);

        }
    }

    public void destroyP8()
    {
        foreach (var paper8 in papers8)
        {
            paper8.SetActive(false);

        }
    }
}
