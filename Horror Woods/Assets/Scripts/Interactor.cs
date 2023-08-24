using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Interactor : MonoBehaviour
{
    public LayerMask interactibleLayerMask = 6;
    public Interactable interactable;
    public Image interactImage;
    public Sprite defaultIcon;
    public Vector2 defaultIconSize;
    public Sprite defaultInteractIcon;
    public Vector2 defaultInteractIconSize;

    public TMP_Text notesText;

    public int notesFound;
    float randomValue;

    public GameObject monsterPrefab;
    public Transform monsterSpawner;

    public AudioClip paperGrab;
    public AudioSource audio;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactibleLayerMask))
        {
            if(hit.collider.GetComponent<Interactable>() != false)
            {
                if(interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                }
                if(interactable.interactIcon != null)
                {
                    interactImage.sprite = interactable.interactIcon;
                    if (interactable.iconSize == Vector2.zero)
                    {
                        interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                    }
                    else
                    {
                        interactImage.rectTransform.sizeDelta = interactable.iconSize;
                    }
                }
                else
                {
                    interactImage.sprite = defaultInteractIcon;
                    interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                }
                    
                if(Input.GetKeyDown(KeyCode.F))
                {                
                    interactable.onInteract.Invoke();

                    audio.Play();

                    notesFound += 1;

                    if (notesFound == 8)
                    {
                        SceneManager.LoadScene("WinScene");
                    }

                    randomValue = Random.value;
                    if(randomValue > 0.5)
                    {
                        Instantiate(monsterPrefab, monsterSpawner.position, Quaternion.identity);
                        monsterPrefab.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if(interactImage.sprite != defaultIcon)
            {
                interactImage.sprite = defaultIcon;
                interactImage.rectTransform.sizeDelta = defaultIconSize;
            }
        }

        notesText.text = notesFound.ToString() + " /8";
    }
}
