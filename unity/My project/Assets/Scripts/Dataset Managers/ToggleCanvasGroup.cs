using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvasGroup : MonoBehaviour
{
    public GameObject menu;
    // This method will be set in the inspector in the OnClick section of your button
    public void ToggleCanvasGroupActive()
    {
        // This will set the canvas group to active if it is inactive OR set it to inactive if it is active
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
    }
}
