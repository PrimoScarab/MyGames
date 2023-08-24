using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSwap : MonoBehaviour
{
    [SerializeField]
    GameObject ground;

    [SerializeField]
    Material newGroundMaterial;

    public Material newMat;

    [SerializeField]
    Material oldGroundMaterial;

    MeshRenderer meshRendererGround;

    Renderer[] children;

    [SerializeField]
    GameObject walls;

    // Start is called before the first frame update
    void Start()
    {
        meshRendererGround = ground.GetComponent<MeshRenderer>();
        oldGroundMaterial = meshRendererGround.material;
        newGroundMaterial.mainTextureScale = oldGroundMaterial.mainTextureScale;

        //children = GetComponentsInChildren<Renderer>();
        children = walls.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            swapGraphics();
        }

        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
        }

    private void swapGraphics()
    {
        meshRendererGround.material = meshRendererGround.material.name.StartsWith(newGroundMaterial.name) ? oldGroundMaterial : newGroundMaterial;
    }
}
