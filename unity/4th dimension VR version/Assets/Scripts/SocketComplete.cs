using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketComplete : MonoBehaviour
{
    public int socketCount;
    public GameObject canvasComplete;

    public void IncrementCount()
    {
        socketCount++;
        CheckSortingComplete();
    }

    public void DecrementCount()
    {
        socketCount--;
        CheckSortingComplete();
    }

    public void CheckSortingComplete()
    {
        if (socketCount==7)
        {
            canvasComplete.SetActive(true);
        }
        else
        {
            canvasComplete.SetActive(false);
        }
    }
}
