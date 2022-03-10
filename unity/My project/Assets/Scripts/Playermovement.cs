using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Rigidbody rb;
    private float hozInput, vertInput;
    [SerializeField] private float speed = 10;
    [SerializeField] private float speedUp = 50;
    // Start is called before the first frame update

    private bool isUpButtonPressed = false;
    private bool isDownButtonPressed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        hozInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isDownButtonPressed = true; 
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUpButtonPressed = true;
        }
    }

    private void FixedUpdate() //gets called every fixed amount of time and not every frame
    {
        Vector3 playerMovement = new Vector3(hozInput, 0, vertInput); //Creates direction vector for our movement 
        playerMovement *= speed; //change the length of the vector
        rb.AddForce(playerMovement, ForceMode.Acceleration); //add a force towards the direction indicated by the vector
        
        if (isUpButtonPressed == true) //Going upwards
        {
            rb.AddForce(Vector3.up*speedUp, ForceMode.Acceleration);
            isUpButtonPressed = false;
        }

        if (isDownButtonPressed == true)
        {
            rb.AddForce(Vector3.down * speedUp, ForceMode.Acceleration);
            isDownButtonPressed = false;
        }
    }
}
