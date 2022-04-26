using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class XRTeleportationController : MonoBehaviour
{
    public InputActionReference teleportActionReference;

    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;


    // Update is called once per frame
    void Start()
    {
        teleportActionReference.action.performed += TeleportModeActivated;
        teleportActionReference.action.canceled += TeleportModeCanceled;
    }

    private void TeleportModeCanceled(InputAction.CallbackContext obj) => Invoke("DeactivateTeleporter", 0.1f);

    private void DeactivateTeleporter()
    {
        if (enabled) { onTeleportCancel.Invoke(); }
    }

    private void TeleportModeActivated(InputAction.CallbackContext obj)
    {
        if (enabled) { onTeleportActivate.Invoke(); }
    }

}
