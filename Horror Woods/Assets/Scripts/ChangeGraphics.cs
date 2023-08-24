using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGraphics : MonoBehaviour
{
    public GameObject tree_2d;
    public GameObject tree_3d;
    public GameObject realisticPapers;
    public GameObject retroPapers;
    GameObject[] grounds;

    public Material forestGound;
    public Material forestGroundRetro;

    MeshRenderer[] tree3d_MR;
    SpriteRenderer[] realisticPapers_SR;
    SpriteRenderer[] tree2d_SR;
    SpriteRenderer[] retroPapers_SR;


    public bool retroGraphics = false;
    public bool tree2dEnabled = false;
    public bool tree3dEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
        tree3d_MR = tree_3d.GetComponentsInChildren<MeshRenderer>();
        realisticPapers_SR = realisticPapers.GetComponentsInChildren<SpriteRenderer>();
        tree2d_SR = tree_2d.GetComponentsInChildren<SpriteRenderer>();
        retroPapers_SR = retroPapers.GetComponentsInChildren<SpriteRenderer>();
        grounds = GameObject.FindGameObjectsWithTag("ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(tree2dEnabled == true)
            {
                retroGraphics = false;
                tree2dEnabled = false;
                tree3dEnabled = true;
                
                foreach(var ground in grounds)
                {
                    ground.GetComponent<MeshRenderer>().material = forestGound;
                }

                foreach (var tree3d in tree3d_MR)
                {
                    tree3d.enabled = true;
                }
                foreach(var realP in realisticPapers_SR)
                {
                    realP.enabled = true;
                }
                foreach(var tree2d in tree2d_SR)
                {
                    tree2d.enabled = false;
                }
                foreach(var retroP in retroPapers_SR)
                {
                    retroP.enabled = false;
                }

            }
            else if(tree3dEnabled == true)
            {
                retroGraphics = true;
                tree3dEnabled = false;
                tree2dEnabled = true;
                
                foreach (var ground in grounds)
                {
                    ground.GetComponent<MeshRenderer>().material = forestGroundRetro;
                }

                foreach (var tree3d in tree3d_MR)
                {
                    tree3d.enabled = false;
                }
                foreach (var realP in realisticPapers_SR)
                {
                    realP.enabled = false;
                }
                foreach (var tree2d in tree2d_SR)
                {
                    tree2d.enabled = true;
                }
                foreach (var retroP in retroPapers_SR)
                {
                    retroP.enabled = true;
                }
            }
            
        }
    }
}
