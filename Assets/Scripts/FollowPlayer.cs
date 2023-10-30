using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public bool cameraToggle = false;
    private float rotationWidth;
    [SerializeField] private Vector3 defaultOffset = new Vector3(0, 7, -9);
    [SerializeField] private Vector3 alternateOffset = new Vector3(-0.1f, 2.5f, 0.85f);

    // Start is called before the first frame update
    void Start()
    {
        rotationWidth = player.GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If the player presses 'V' and the camera toggle is set to off
        if (Input.GetKeyDown(KeyCode.V) && cameraToggle == false)
        {
            cameraToggle = true; // Switch to alternate camera position

        }
        else if (Input.GetKeyDown(KeyCode.V) && cameraToggle == true)
        {
            cameraToggle = false;
        }

        if (!cameraToggle)
        {
            CameraPositionDefault();
        }
        else
        {
            CameraPositionAlternate();
        }
    }

    void CameraPositionDefault()
    {
        // Offset the camera above the vehicle
        transform.position = player.transform.position + defaultOffset;
    }

    void CameraPositionAlternate()
    {
        // Offset the camera above the vehicle
        transform.position = player.transform.position + alternateOffset;
    }
}
