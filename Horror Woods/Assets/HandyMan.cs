using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HandyMan : MonoBehaviour
{
    private NavMeshAgent enemy;

    private Transform PlayerTarget;

    Camera scareCam;
    AudioSource scareSound;

    public ChangeGraphics changeGraphics;

    GameObject Player;

    public GameObject mesh;
    public GameObject joint;




    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        PlayerTarget = GameObject.Find("Player").transform;
        //scareCam = GameObject.FindGameObjectWithTag("scareCam").GetComponent<Camera>() as Camera;
        //scareCam.gameObject.SetActive(false);
        //StartCoroutine(kill());
        scareSound = GetComponent<AudioSource>();
        changeGraphics = GetComponent<ChangeGraphics>();
        Player = GameObject.Find("Player");
        Player.GetComponent<ChangeGraphics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<ChangeGraphics>().retroGraphics == true)
        {
            mesh.SetActive(false);
            joint.SetActive(false);
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            //gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Player.GetComponent<ChangeGraphics>().retroGraphics == false)
        {
            mesh.SetActive(true);
            joint.SetActive(true);
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            //gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }


        Destroy(gameObject, 5);

        enemy.destination = PlayerTarget.position;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            //StartCoroutine(kill());
            //scareCam.gameObject.SetActive(true);
            scareSound.Play();
            //kill();

        }
    }


    IEnumerator kill()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("DeathScene");
    }
}