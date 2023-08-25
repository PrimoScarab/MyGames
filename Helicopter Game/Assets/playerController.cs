using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [SerializeField]
    protected Transform Helicopter;

    [SerializeField]
    protected Transform rotor;

    [SerializeField]
    protected Transform rotorBack;

    public Rigidbody rb;
    
    public float moveSpeed;
    public float sideSpeed;
    public float upSpeed;
    public float turnSpeed;
    public float rotorSpeed;

    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded == false)
        {
            PlayerMovement();
        }

        playerFlight();
    }

  
    void PlayerMovement()
    { 
            if (Input.GetKey("w"))
            {
                rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(transform.right * -sideSpeed * Time.deltaTime);
            }

            if (Input.GetKey("d"))
            {
                rb.AddForce(transform.right * sideSpeed * Time.deltaTime);
            }

            if (Input.GetKey("s") && isGrounded != true)
            {
                rb.AddForce(transform.forward * -moveSpeed * Time.deltaTime);
            }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    void playerFlight()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rotor.transform.Rotate(new Vector3(0f, rotorSpeed, 0f) * Time.deltaTime);
            rotorBack.transform.Rotate(new Vector3(rotorSpeed, 0f, 0f) * Time.deltaTime);
            rb.AddForce(0, upSpeed * Time.deltaTime, 0);
        }

        else if (!Input.GetKey(KeyCode.Space))
        {
            rotor.transform.Rotate(new Vector3(0f, 400f, 0f) * Time.deltaTime);
            rotorBack.transform.Rotate(new Vector3(400f, 0f, 0f) * Time.deltaTime);
            rb.AddForce(0, (-upSpeed / 2) * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
