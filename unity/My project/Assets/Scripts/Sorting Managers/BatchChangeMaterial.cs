using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchChangeMaterial : MonoBehaviour
{
    public Material transparentMaterial;
    void Start ()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
            {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<MeshRenderer>().material=transparentMaterial;
            }

    }
}

 