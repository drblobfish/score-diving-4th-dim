using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public Transform playerBody;
    public float xRotation= 0f;
    public float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //frame rate independant
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        //making sure we can only look 90° up and down to not get sick 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerBody.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);
        //playerBody.Rotate(Vector3.left * mouseY);


    }
}
