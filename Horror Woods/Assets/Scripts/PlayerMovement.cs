using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound;
    public TMP_Text staminaText;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float stamina;
    float maxStamina;
    public Slider staminaBar;
    public float dVAlue;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        stamina = 100;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        if (grounded)
        {
            rb.drag = groundDrag;

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if(Input.GetKey(KeyCode.LeftShift) && stamina > 10)
                {    
                    moveSpeed = 10f;
                    footstepsSound.enabled = false;
                    sprintSound.enabled = true;
                    StartCoroutine(loseStamina());
                    
                }
                else
                {
                    moveSpeed = 7f;
                    footstepsSound.enabled = true;
                    sprintSound.enabled = false;
                    
                }
                
            }
            else
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = false;
                stamina += 1;
            }

        }
        else
        {
            rb.drag = 0;
        }

        if(stamina >= 100)
        {
            stamina = 100;
        }
        else if(stamina <= 0)
        {
            stamina = 0;
        }

        staminaText.text = stamina.ToString();
            
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private IEnumerator loseStamina()
    {
        yield return new WaitForSeconds(2);
        stamina -= 1;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
