using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetAnim : MonoBehaviour
{
    [SerializeField] private GameObject mesh;
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = mesh.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
