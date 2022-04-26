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
    }

    public void DecrementCount()
    {
        socketCount--;
    }

    public void Update()
    {
        if (socketCount==7)
        {
            canvasComplete.SetActive(true);
        }
    }
}
