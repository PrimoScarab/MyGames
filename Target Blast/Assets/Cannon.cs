using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed = 1; //Variabel som har värdet "1"
    public float RotAngleY = 90; //Variabel som har värdet "90"


    // Update is called once per frame
    void Update()
    {
        float rY = Mathf.SmoothStep(-45, RotAngleY, Mathf.PingPong(Time.time * speed, 1)); //Variabel med ett värde som lent växlar mellan "-45" och värdet från RotAngleY
        transform.rotation = Quaternion.Euler(0, 0, rY); //Roterar objektets z-axel, värdet som z-axeln får är "rY" 
    }
}
