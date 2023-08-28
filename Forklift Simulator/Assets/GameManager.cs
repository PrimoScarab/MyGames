using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects;
    public int objectsPlaced = 0;
    public int MaxObjectsPlaced = 0;


    public GameObject PauseMenuPanel;
    public GameObject ResultPanel;
    public GameObject ResetPalletPanel;

    public WheelController WC;

    public GameObject needle;
    private float startPosition = 220f;
    private float endPosition = -41f;

    private float desiredPosition;

    public float vehicleSpeed;
    bool paused = false;

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    private float startTime;
    public bool finnished = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseMenuPanel.SetActive(false);
        ResultPanel.SetActive(false);
        ResetPalletPanel.SetActive(false);
        startTime = Time.time;

        
        //Gets all joystick names
        string[] names = Input.GetJoystickNames();
        

        //Writes out all connected joysticks
        for(int i = 0; i < names.Length; i++)
        {
            Debug.Log("Connected joystick " + names[i] + ": " + i);
        }

    }

    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //Score
        if(objectsPlaced == MaxObjectsPlaced)
        {
            Time.timeScale = 0f;
            ResultPanel.SetActive(true);
            finnished = true;
        }

        Timer();

    }

    void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        PauseMenuPanel.SetActive(false);
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        PauseMenuPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPallets()
    {
        PauseMenuPanel.SetActive(false);
        ResetPalletPanel.SetActive(true);
    }

    public void ResetPalletsBack()
    {
        PauseMenuPanel.SetActive(true);
        ResetPalletPanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void FixedUpdate()
    {
        vehicleSpeed = WC.KPH;
        
        updateNeedle();
    }

    private void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
    }

    private void Timer()
    {
        if (finnished)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }
}
