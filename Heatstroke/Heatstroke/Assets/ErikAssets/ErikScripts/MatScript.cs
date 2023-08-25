using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatScript : MonoBehaviour
{

    Color[] colors;

    Renderer thisRend; //Renderer of our Cube

    float transitionTime = 60f; // Amount of time it takes to fade between colors

    void Start()

    {

        thisRend = GetComponent<Renderer>(); // grab the renderer component on our cube

        colors = new Color[2]; // We will randomize through this array

        //initialize our array indexes with colors

        colors[0] = new Color32(169, 169, 169, 255);

        colors[1] = new Color32(233, 111, 92, 255);

        //start our coroutine when the game starts

        StartCoroutine(ColorChange());

    }

    void Update()

    {

    }

    IEnumerator ColorChange()

    {

        //Infinite loop will ensure our coroutine runs all game

        while (true)

        {

            Color newColor = colors[1]; // Assign newColor to a random color from our array

            float transitionRate = 0; //Create and set transitionRate to 0. This is necessary for our next while loop to function

            /* 1 is the highest value that the Color.Lerp function uses for

             * transitioning between two colors. This while loop will execute

             * until transitionRate is incremented to 1 or higher

             */

            while (transitionRate < 1)

            {

                //this next line is how we change our material color property. We Lerp between the current color and newColor

                thisRend.material.SetColor("_Color", Color.Lerp(thisRend.material.color, newColor, Time.deltaTime * transitionRate));

                transitionRate += Time.deltaTime / transitionTime; // Increment transitionRate over the length of transitionTime

                yield return null; // wait for a frame then loop again

            }

            yield return null; // wait for a frame then loop again

        }

    }

}


