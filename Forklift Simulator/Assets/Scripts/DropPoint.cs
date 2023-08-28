using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCol;
    BoxCollider[] childCols;
    Transform[] children;
    Rigidbody[] childRB;
    public GameObject Object;
    public Transform forks;
    public bool objectPlaced = false;
    public bool pickedup;
    float vertical;

    GameObject gameManagerObject;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        
        children = GetComponentsInChildren<Transform>();
        childRB = GetComponentsInChildren<Rigidbody>();

        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        vertical = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            //Debug.Log("detected");
            pickedup = true;
            transform.SetParent(forks.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        pickedup = false;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DropPoint" && objectPlaced == false)
        {
            gameManager.objectsPlaced += 1;
            objectPlaced = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DropPoint" && objectPlaced == true)
        {
            gameManager.objectsPlaced -= 1;
            objectPlaced = false;
        }
    }

}
