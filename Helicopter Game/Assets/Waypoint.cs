using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public GameObject[] Ring;
    public Transform playerLoc;
    public Camera cam;
    public GUIStyle style;

    public Vector3[] ringLoc;

    void Start()
    {
        Ring = GameObject.FindGameObjectsWithTag("Ring");
        ringLoc = new Vector3[Ring.Length];
    }

    void Update()
    {
        for (int i = 0; i < Ring.Length; i++)
        {
            ringLoc[i] = Camera.main.WorldToScreenPoint(Ring[i].transform.position);
        }
    }

    void OnGUI()
    {
        foreach (GameObject ring in Ring)
        {
            float thing = Vector3.Dot((ring.transform.position - cam.transform.position).normalized, cam.transform.forward);

            if (ring.activeSelf && thing > 0)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(ring.transform.position);
                GUI.color = Color.cyan;
                GUI.Label(new Rect(screenPos.x + 6, Screen.height - screenPos.y, 100, 20), " ring", style);
                GUI.Label(new Rect(screenPos.x - 6, Screen.height - screenPos.y, 100, 20), "○", style);
            }

        }

    }
}
