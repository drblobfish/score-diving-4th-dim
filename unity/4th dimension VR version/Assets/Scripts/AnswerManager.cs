using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private float spacing = 10.0F;
    [SerializeField] private GameObject[] answersObjects;
    private GameObject[] answers ;
    private Vector3[] grid;
    private System.Random _random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        answers = new GameObject[answersObjects.Length];

        grid = Grid(2,7);
        Shuffle(grid);
        
        for (int i = 0; i < answersObjects.Length; i++)
        {
            GameObject answerParent = Instantiate(answersObjects[i],grid[i],gameObject.transform.rotation,gameObject.transform);
            GameObject child = answerParent.transform.GetChild(0).gameObject;
            child.name = answerParent.name;
            child.transform.parent = gameObject.transform;
            Destroy(answerParent);
            child.AddComponent<BoxCollider>();
            child.tag = "Dataset";
            answersObjects[i]=child;
        }
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
    void Shuffle(Vector3[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            Vector3 t = array[r];
            array[r] = array[n];
            array[n] = t;
    }
}
}
