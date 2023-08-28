using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageSender : MonoBehaviour
{

    bool HiDisplayed = false;
    bool HelloDisplayed = false;
    public TextMeshProUGUI textMesh;
    private char delimiter = '*';

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && HiDisplayed == false)
        {
            DisplayMessage("Hi");
        }
        if (Input.GetKey(KeyCode.Space) && HelloDisplayed == false)
        {
            DisplayMessage("Hi");
        }

    }

    public void DisplayMessage(string msg)
    {
        msg = delimiter + " " + msg + "\n";        // add the delimiter and a space to the start of the message string, and a new line character to the end
        textMesh.text = msg + textMesh.text;
        StartCoroutine(Waiter());
        StartCoroutine(DisplayMessageRoutine());
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(5);
        HiDisplayed = true;
        HelloDisplayed = true;
    }

    private IEnumerator DisplayMessageRoutine()
    {

        //HiDisplayed = true;
        //HelloDisplayed = true;
        yield return new WaitForSecondsRealtime(5);
        string tmp = textMesh.text;
        bool delimiterReached = false;

        HiDisplayed = false;
        HelloDisplayed = false;

        while(!delimiterReached)
        {
            if (tmp[tmp.Length - 1] == delimiter)
                delimiterReached = true;

            tmp = tmp.Substring(0, tmp.Length - 1);
        }

        textMesh.text = tmp;

    }
}
