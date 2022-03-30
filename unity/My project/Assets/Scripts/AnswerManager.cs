using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private float spacing = 10.0F;
    [SerializeField] private GameObject[] answersObjects;
    private Vector3[] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = Grid(2,7);
        
        for (int i = 0; i < answersObjects.Length; i++)
        {
            Instantiate(answersObjects[i],grid[i],gameObject.transform.rotation);
        }
        /*
        foreach (GameObject answerObject in answersObjects)
        {
            Instantiate(answerObject,);
        }*/
    }

    private Vector3[] Grid(int n, int m)
    {
        Vector3[] grid = new Vector3[n*m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m ; j++)
            {
                grid[(i*m)+j] = gameObject.transform.position + Vector3.forward * spacing * i + Vector3.right *spacing* j;
            }
        }
        return grid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
