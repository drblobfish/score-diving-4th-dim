using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestSocket : MonoBehaviour
{
    
    public string selectedObjectName;
    public GameObject selectionParent ;

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
        selectionParent.GetComponent<SocketComplete>().IncrementCount();
        Debug.Log("Socket Selected: "+ selectedObjectName);
        Debug.Log("Count: " + selectionParent.GetComponent<SocketComplete>().socketCount);
    }

    public void OnSocketSelectExit()
    {
        selectionParent.GetComponent<SocketComplete>().DecrementCount(); ; 
        Debug.Log("Socket Selected Exit: "+selectedObjectName);
        Debug.Log("Count: " + selectionParent.GetComponent<SocketComplete>().socketCount);
        selectedObjectName = "";
    }
}
