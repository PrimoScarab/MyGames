using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour
{
    public GameObject ringPrefab;
    public BoxCollider boxColl;
    public MeshCollider meshColl;
    public GameObject[] rings;



    public Text scoreText;
    public static float score;
    static public bool ringTouched = false;
    

    // Start is called before the first frame update
    void Start()
    {
        boxColl = ringPrefab.GetComponent<BoxCollider>();
        meshColl = ringPrefab.GetComponent<MeshCollider>();
        score = 0;
        rings = GameObject.FindGameObjectsWithTag("Ring");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Ring").Length > 0)
        {
            scoreText.text = "Rings: " + score.ToString("f0") + "/10";
        }
        else
        {
            scoreText.text = "Finished";
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ring")
        {
            score++;
            collider.gameObject.SetActive(false);
        }
    }
}
