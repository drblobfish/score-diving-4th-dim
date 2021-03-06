using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private bool isWatching ;
    public Camera cam;
    public Transform target;
    public float distanceToTarget = 3;
    public float panSpeed = 20f;
    //public GameObject camGameObject;
    private Vector3 cameraPos;
    private Vector3 previousPosition;
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?

    void Update()
    {
        if (target)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                cam.transform.Translate(new Vector3(0, 0, +distanceToTarget));
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            }

            if (Input.GetMouseButtonDown(0))
            {
                isRotating = true;
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonDown(2))
            {
                isPanning = true;
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }

            else if (isRotating)
            {
                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;

                float rotationAroundZYAxis = direction.x * 180; // object moves horizontally
                float rotationAroundXAxis = -direction.y * 180; // object moves vertically

                target.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis, Space.World);
                if (isWatching)
                {
                    target.transform.Rotate(new Vector3(0, 1, 0), rotationAroundZYAxis, Space.World); 
                }
                else if (!isWatching)
                {
                    target.transform.Rotate(new Vector3(0, 0, 1), rotationAroundZYAxis, Space.World); 
                }
                
                previousPosition = newPosition;
            }

            if (isPanning)
            {
                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Vector3 pos = previousPosition - newPosition;

                Vector3 move = new Vector3(pos.x*panSpeed, pos.y*panSpeed, 0);
                cam.transform.Translate(move, Space.Self);
                previousPosition = newPosition;
            }

            // Disable movements on button release

            if (!Input.GetMouseButton(0)) isRotating = false;
            if (!Input.GetMouseButton(2)) isPanning = false;   
        }
    }
}