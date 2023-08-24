using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent enemy;

    private Transform PlayerTarget;

    public Camera scareCam;
    AudioSource scareSound;

    public ChangeGraphics changeGraphics;

    GameObject Player;
    public GameObject handyman;




    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        PlayerTarget = GameObject.Find("Player").transform;
        scareCam.gameObject.SetActive(false);
        //StartCoroutine(kill());
        scareSound = GetComponent<AudioSource>();
        changeGraphics = GetComponent<ChangeGraphics>();
        Player = GameObject.Find("Player");
        Player.GetComponent<ChangeGraphics>();
        handyman.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<ChangeGraphics>().retroGraphics == true)
        {
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            handyman.SetActive(false);
        }
        else if (Player.GetComponent<ChangeGraphics>().retroGraphics == false)
        {
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            handyman.SetActive(true);
        }

        
            Destroy(gameObject, 5);

        enemy.destination = PlayerTarget.position;  
        
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            StartCoroutine(kill());
            scareCam.gameObject.SetActive(true);
            scareSound.Play();
            kill();
            
        }
    }
    

    IEnumerator kill()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("DeathScene");
    }
}
