using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float distanceToTarget = 3;
    public float panSpeed = 10f;
    public GameObject camGameObject;
    private Vector3 cameraPos;
    private Vector3 previousPosition;
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.transform.Translate(new Vector3(0, 0, +distanceToTarget));
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
        }

        if (Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRotating = true;
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isPanning = true;
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        else if (isRotating)

        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = direction.x * 180; // object moves horizontally
            float rotationAroundXAxis = -direction.y * 180; // object moves vertically

            target.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis, Space.World);
            target.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); 

            previousPosition = newPosition;
            Debug.Log("is rotating");
        }

        if (isPanning)
        {
            Vector3 pos =cam.ScreenToViewportPoint(Input.mousePosition - previousPosition);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
            Debug.Log("is panning");
        }

        // Disable movements on button release

        if (!Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.LeftShift)) isRotating = false;
        if (!Input.GetKeyDown(KeyCode.LeftShift)) isPanning = false;   
    }
}