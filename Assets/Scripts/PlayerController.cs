using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private variables
    [SerializeField] private float speed;
    [SerializeField] private float rpm;
    [SerializeField] private float horsePower = 0.0f;
    [SerializeField] private float turnSpeed = 100.0f;
    [SerializeField] int wheelsOnGround;
    private float horizontalInput;
    private float forwardInput;

    [SerializeField] List<WheelCollider> allWheels;

    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Retrieve the player's rigidbody component
        playerRb.centerOfMass = centerOfMass.transform.position; // Set the player's center of mass to the center of mass object's position in the hierarchy
    }

    // FixedUpdate is called before Update and is used when calculating physics
    void FixedUpdate()
    {
        // If all 4 wheels are on the ground
        if (IsOnGround())
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            // Moves the vehicle forward/backward from player's input
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            // Apply a forward force relative to the object's coordinates to move the vehicle forward based on the player's input
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);

            // Rotates the vehicle left/right from player's input
            transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); // Calculate the player's speed in mph and round to nearest integer
            speedometerText.SetText("Speed: " + speed + "mph");

            rpm = Mathf.Round((speed % 30) * 40); // Simulates gear shifting
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0; // Keeps track of the number of wheels on the ground

        // This will go through every wheel on the car
        foreach (WheelCollider wheel in allWheels)
            if (wheel.isGrounded) // If a wheel is on the ground
                wheelsOnGround++;

        if (wheelsOnGround == 4) // If all 4 wheels are on the ground
            return true;
        else // If any wheel is off of the ground
            return false;
    }
}
