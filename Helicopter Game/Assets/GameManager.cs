using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ringPrefab;
    public GameObject helicopter;

     public BoxCollider boxCol;
     public BoxCollider playerCol;

    public Text ScoreText;

    static public float Score;
    
    // Start is called before the first frame update
    void Start()
    {
       boxCol = ringPrefab.GetComponent<BoxCollider>();
        playerCol = helicopter.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString("F0");
    }

    void OnTriggerEnter(Collider collider)
    {
        if(playerCol == boxCol)
        {
            Debug.Log("Detected");
        }
    }
}
