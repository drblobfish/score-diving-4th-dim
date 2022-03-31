using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestSocket : MonoBehaviour
{
    public void OnSocketHoverEnter(HoverEnterEventArgs args)
    {
        Debug.LogFormat("Socket Hovered by: {0}", args.interactable.gameObject.GetComponent<identity>().name);
        //Debug.Log(args.interactorObject);
    }

    public void OnSocketHoverExit()
    {
        Debug.Log("Socket Hovered Exited");
    }

    public void OnSocketSelectEnter()
    {
        Debug.Log("Socket Selected");
    }

    public void OnSocketSelectExit()
    {
        Debug.Log("Socket Seclted Exit");
    }
}
