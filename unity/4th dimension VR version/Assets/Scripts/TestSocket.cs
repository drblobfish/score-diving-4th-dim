using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestSocket : MonoBehaviour
{
    public string selectedObjectName;
    public void OnSocketHoverEnter(HoverEnterEventArgs args)
    {
        Debug.LogFormat("Socket Hovered by: {0}", args.interactable.gameObject.name);
        
        //Debug.Log(args.interactorObject);
    }

    public void OnSocketHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("Socket Hovered Exited:"+ args.interactable.gameObject.name);
    }

    public void OnSocketSelectEnter(SelectEnterEventArgs args)
    {
        selectedObjectName = args.interactable.gameObject.name;
        Debug.Log("Socket Selected: "+ selectedObjectName);
    }

    public void OnSocketSelectExit()
    {
        Debug.Log("Socket Selected Exit: "+selectedObjectName);
        selectedObjectName = "";
    }
}
