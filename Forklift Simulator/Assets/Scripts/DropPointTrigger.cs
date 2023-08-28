using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointTrigger : MonoBehaviour
{
    BoxCollider trigger;
    public bool hasChild = false;

    MeshRenderer renderer;
    Material material;
    Color32 White;
    Color32 Green;
    public GameObject Arrow;
    public GameObject checkMark;
    

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
        renderer = transform.parent.GetComponent<MeshRenderer>();
        material = renderer.material;


        material.SetFloat("_Mode", 4f);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;


        //col = renderer.material.GetColor("_Color");
        White = new Color32(255, 255, 255, 90);
        Green = new Color32(0, 255, 0, 90);
        //col.a = 100;
        renderer.material.SetColor("_Color", White);
        //renderer.material.SetColor("_Color", Green);

        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.childCount > 0)
        {
            Debug.Log("HI");
            hasChild = true;
            renderer.material.SetColor("_Color", Green);
            Arrow.SetActive(false);
            checkMark.SetActive(true);
            
        }
        else
        {
            hasChild = false;
            White = new Color32(255, 255, 255, 90);
            Arrow.SetActive(true);
        }
        */
    }

        public void disableTrigger()
        {
            trigger.enabled = false;
        }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WoodObject")
        {
            renderer.material.SetColor("_Color", Green);
            Arrow.SetActive(false);
            checkMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "WoodObject")
        {
            White = new Color32(255, 255, 255, 90);
            renderer.material.SetColor("_Color", White);
            Arrow.SetActive(true);
            checkMark.SetActive(false);
        }
    }
}
