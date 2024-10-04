using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    // Zoom Variables
    public float zoomSpeed = 2f;        // Speed of zooming
    public float minZoom;          // Minimum zoom level
    public float maxZoom;         // Maximum zoom level

    private Camera cam;                 // Reference to the Camera component

    void Start()
    {
        cam = GetComponent<Camera>();   // Get the Camera component
    }

    void Update()
    {
        // Follow the player
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

        // Zoom in/out with mouse scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scrollInput * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}
