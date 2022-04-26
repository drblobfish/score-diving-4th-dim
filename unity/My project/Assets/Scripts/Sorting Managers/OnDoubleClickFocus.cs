using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace draganddrop.raycast
{  
    public class OnDoubleClickFocus : MonoBehaviour
    {
        float doubleClickTime = .2f, lastClickTime;
        public Camera cam ;
        private Vector3 camBasePos ;
        private RotateObject rotateObject; // script to rotate object
        [SerializeField] private GameObject focusPosition ; //Empty where to move double clicked dataset.
        private GameObject onfocusObject;
        private Vector3 previousPos;
        public bool Onfocus = false ;

        void Start()
        {
            rotateObject = gameObject.GetComponent<RotateObject>();
            camBasePos = cam.transform.position;
        }

        void Update()
        {   
            // Detect Double click
            if (Input.GetMouseButtonDown(0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;

                if (timeSinceLastClick <= doubleClickTime)
                {
                    OnDoubleCLick("Dataset");
                }

                lastClickTime = Time.time;
            }
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClick("Dataset") ;
            }
        }

        void OnRightClick(string tag)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Onfocus && Physics.Raycast(ray, out hit) && hit.collider.tag == tag)
            { 
                // Put this object to foreground and activate rotation on it
                rotateObject.target = null ;
                Onfocus = false ;
                onfocusObject = null ;
            }
        }
        void OnDoubleCLick(string tag)
        {
            if (Onfocus) // if already focusing, stop
            {
                rotateObject.target = null ;
                Onfocus = false ;
                cam.transform.position = camBasePos ;
            }
            else // if not ray cast to find an object
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.tag == tag)
                { 
                    //Enable the focusing mechanism.
                    Onfocus = true;
                    // Put this object to foreground and activate rotation on it
                    rotateObject.target = hit.transform;
                    onfocusObject = hit.collider.gameObject;
                    previousPos = onfocusObject.transform.position;
                    onfocusObject.transform.position = focusPosition.transform.position;
                }
            }
        }
    }
}