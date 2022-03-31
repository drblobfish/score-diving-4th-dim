using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoubleClickFocus : MonoBehaviour
{
    float doubleClickTime = .2f, lastClickTime;

    [SerializeField] private Camera cam ;
    [SerializeField] private GameObject focusPosition;
    private RotateObject rotateObject;

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
        if (Onfocus)
        {
            rotateObject.target=null;
            Onfocus = false;
            onfocusObject.transform.position = previousPos;
        }
        else
        {
            Onfocus = true;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                rotateObject.target = hit.transform;
                onfocusObject = hit.collider.gameObject;
                previousPos = onfocusObject.transform.position;
                onfocusObject.transform.position = focusPosition.transform.position;
            }
        }
    }
}
