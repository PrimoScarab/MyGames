using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript2 : MonoBehaviour
{
    public CharacterController controller;
    float speed = 6f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
