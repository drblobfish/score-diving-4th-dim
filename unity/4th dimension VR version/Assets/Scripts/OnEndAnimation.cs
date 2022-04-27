using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEndAnimation : MonoBehaviour
{
    public DatasetAnim datasetAnim;

    public void EndAnimation()
    {
        datasetAnim.EndAnimation();
    }

}
