using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoubleClickFocus : MonoBehaviour
{
    // DOuble click
    float doubleClickTime = .2f, lastClickTime;

    [SerializeField] private Camera cam ;
    [SerializeField] private GameObject focusPosition; // empty positioned where we want to put the object in focus
    private RotateObject rotateObject; // script to rotate object

    private GameObject onfocusObject;
    private Vector3 previousPos;

    private bool Onfocus = false;

    // Start is called before the first frame update
    void Start()
    {
        rotateObject = gameObject.GetComponent<RotateObject>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Detect Double click
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickTime)
            {
                Debug.Log("Double click");
                OnDoubleCLick();
            }
                
            else
                Debug.Log("Normal click");

            lastClickTime = Time.time;
        }
    }

    void OnDoubleCLick()
    {
        if (Onfocus) // if already focusing, stop
        {
            rotateObject.target=null;
            Onfocus = false;
            onfocusObject.transform.position = previousPos;
        }
        else // if not ray cast to finb an object
        {
            Onfocus = true;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            { // put this object to foreground and activate rotation on it
                rotateObject.target = hit.transform;
                onfocusObject = hit.collider.gameObject;
                previousPos = onfocusObject.transform.position;
                onfocusObject.transform.position = focusPosition.transform.position;
            }
        }
    }
}
