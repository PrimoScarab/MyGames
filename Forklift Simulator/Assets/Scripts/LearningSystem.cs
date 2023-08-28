using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class LearningSystem : MonoBehaviour
{
    
    public WheelController WC;
    public Fork_Controlls FC;

    private float timer = 0;

    bool slowDownDisplayed = false;
    bool lowerForksDisplayed = false;
    bool turnTooFastDisplayed = false;
    bool reverseTooFastDisplayed = false;
    bool checkYourSurroundingsDisplayed = false;

    public TextMeshProUGUI textMesh;
    private char delimiter = '*';   // added to message, used to parse messages from text string
    const float _timeToLive = 5;   // the amount of time a message remains on screen

    public void Update()
    {
        
        //Speed too high
        if (WC.KPH > 16 && slowDownDisplayed == false)
        {   
                DisplayMessage("Slow down");
            slowDownDisplayed = true;
        }

        //Speed too high while forks are all the way up
        if(FC.fork.transform.localPosition.y >= FC.maxYmast.y && WC.KPH >= 5f && lowerForksDisplayed == false)
        {
            DisplayMessage("Lower Forks");
            lowerForksDisplayed = true;
        }

        //Right turn too fast
        if (WC.KPH >= 10f && WC.currentTurnAngle >= 60f && turnTooFastDisplayed == false)
        {
            DisplayMessage("Turn too fast!");
            turnTooFastDisplayed = true;
        }
        //Left turn too fast
        if(WC.KPH >= 10f && WC.currentTurnAngle <= -60f && turnTooFastDisplayed == false)
        {
            DisplayMessage("Turn too fast!");
            turnTooFastDisplayed = true;
        }
        
        if(WC.SWcurrentGear == WC.gearReverse && WC.KPH > 10 && reverseTooFastDisplayed == false)
        {
            DisplayMessage("Reverse too fast");
            reverseTooFastDisplayed = true;
        }


        if(WC.SWcurrentGear == WC.gearReverse && checkYourSurroundingsDisplayed == false)
        {
            DisplayMessage("Check your surroundings");
            checkYourSurroundingsDisplayed = true;
        }
    }

    private void FixedUpdate()
    {
        /*
        if(WC.currentTurnAngle > 0)
        {
            targetTurnAngle = 60f;
        }
        else if(WC.currentTurnAngle < 0)
        {
            targetTurnAngle = -60f;
        }

        resTurnAngle = targetTurnAngle - 0f;

        if(resTurnAngle == 15f || resTurnAngle == -15f)
        {
            Debug.Log("Too fast");
        }
        */
    }

    /// <summary>
    /// The string, msg, will be appended to the textfield and removed after _timeToLive seconds
    /// </summary>
    /// <param name="s"></param>
    /// <param name="ttl"></param>
    public void DisplayMessage(string msg)
    {
        
            msg = delimiter + " " + msg + "\n";        // add the delimiter and a space to the start of the message string, and a new line character to the end
            textMesh.text = msg + textMesh.text;               // add the message to the start of the TextMesh component
            StartCoroutine(DisplayMessageRoutine());
        
    }

    private IEnumerator DisplayMessageRoutine()
    {
        
        
        

        yield return new WaitForSecondsRealtime(_timeToLive);

        string tmp = textMesh.text;         // this may be unnecessary...
        bool delimiterReached = false;
        int escape = 200;                   // just in case I screwed up

        slowDownDisplayed = false;
        lowerForksDisplayed = false;
        turnTooFastDisplayed = false;
        reverseTooFastDisplayed = false;
        checkYourSurroundingsDisplayed = false;

        // remove characters from the end of the string until (and including) the delimiter is reached
        while (!delimiterReached && escape > 0)
        {
            if (tmp[tmp.Length - 1] == delimiter)
                delimiterReached = true;

            tmp = tmp.Substring(0, tmp.Length - 1);

            escape--;
            if (escape <= 0)
                Debug.LogError("loop exited by escape case");
        }

        textMesh.text = tmp;
    }


}
