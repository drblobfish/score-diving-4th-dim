using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketComplete : MonoBehaviour
{
    [SerializeField] private int nbSocket;
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
        if (socketCount== nbSocket)
        {
            canvasComplete.SetActive(true);
        }
        else
        {
            canvasComplete.SetActive(false);
        }
    }
}
