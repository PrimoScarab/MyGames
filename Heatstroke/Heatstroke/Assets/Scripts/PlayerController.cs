using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    float audioPitch;
    //float smooth = 5.0f;
    //float tiltAngle = 60.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    public bool itsWarm = true;
    public bool audioIsPlaying = false;

    [SerializeField]
    public Light dirLight;

    [SerializeField]
    public Transform head;

    public Temperature temperature;

    
    public AudioSource soundScource;
    public AudioClip soundStart;
    

    public AudioSource audioScource;
    // Start is called before the first frame update
    void Start()
    {
        temperature = FindObjectOfType<Temperature>();
        controller = GetComponent<CharacterController>();
        audioScource = GetComponent<AudioSource>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

        if (itsWarm)
        {
            temperature.updatedTemperature += 1f * Time.deltaTime;

            if (temperature.updatedTemperature > 20 && !audioIsPlaying)
            {
                soundScource.PlayOneShot(soundStart);
                soundScource.PlayScheduled(AudioSettings.dspTime + soundStart.length);
                audioScource.Play();
                audioIsPlaying = true;

            }

            if (temperature.updatedTemperature > 30 && temperature.updatedTemperature < 37)
            {
                audioScource.pitch = 1.4f;
            }
            else if (temperature.updatedTemperature > 37)
            {
                audioScource.pitch = 1.8f;
            }
            else
            {
                audioScource.pitch = 1.0f;
            }

            if (temperature.updatedTemperature > 43)
            {
                temperature.updatedTemperature = 43;
            }

        }

        else if (!itsWarm)
        {
            temperature.updatedTemperature -= 1f * Time.deltaTime;

            //audioSource.Stop();

            if (temperature.updatedTemperature < 0)
            {
                temperature.updatedTemperature = 0;
            }

            audioScource.Stop();
            audioIsPlaying = false;
            soundScource.Stop();

        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 direction = -dirLight.transform.forward; //Sol.position - transform.position;

        if (Physics.Raycast(head.position, direction.normalized, out hit))
        {
            print("Found an object - distance: " + hit.distance);
            itsWarm = false;
            // hit.transform.gameObject.
        }
        else
        {
            itsWarm = true;
        }
    }


    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);


        if (temperature.updatedTemperature >= 30f && temperature.updatedTemperature < 37f)
        {
            walkSpeed = 4.0f;
        }

        else if (temperature.updatedTemperature >= 37f && temperature.updatedTemperature <= 43f)
        {
            walkSpeed = 2.0f;
        }
        else
        {
            walkSpeed = 6.0f;
        }
    }
}

