using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEndAnimation : MonoBehaviour
{
    [SerializeField] private GameObject dataset;
    private DatasetAnim datasetAnim;
    // Start is called before the first frame update
    void Start()
    {
        datasetAnim = dataset.GetComponent<DatasetAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndAnimation()
    {
        datasetAnim.EndAnimation();
    }

}
