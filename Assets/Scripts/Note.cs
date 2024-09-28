using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject targetObject;     // The outer ring the note is moving toward initially
    public GameObject missZoneObject;   // The miss zone this note moves to after passing the outer ring
    public string noteColor;            // Color identifier for the note
    public float bpm;                   // BPM passed from the NoteSpawner to calculate speed
    private float speed;                // The movement speed of the note

    private bool canBeHit = false;      // Whether the note is in the hit zone and can be hit
    private bool moveToMissZone = false; // Whether the note should move toward the miss zone

    private void Start()
    {
        // Calculate speed based on the BPM
        speed = bpm / 60f * 5f;  // Adjust this value (5f) as needed to control note speed
    }

    private void Update()
    {
        // If the note should move towards the outer ring (initial target) or miss zone
        if (!moveToMissZone && targetObject != null)
        {
            // Move toward the outer ring
            Vector3 targetPosition = targetObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // If the note reaches the outer ring, start moving towards the miss zone
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                moveToMissZone = true;
                canBeHit = false;  // Note can no longer be hit once it's passed the ring
            }
        }

        // If the note has passed the outer ring and should move towards the miss zone
        if (moveToMissZone && missZoneObject != null)
        {
            Vector3 missZonePosition = missZoneObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, missZonePosition, speed * Time.deltaTime);

            // If the note reaches the miss zone, destroy it and count it as a miss
            if (Vector3.Distance(transform.position, missZonePosition) < 0.1f)
            {
                MissNote();
            }
        }

        // Check for key press if the note is within the hit zone
        if (canBeHit && CheckKeyPress())
        {
            HitNote();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the note enters the outer ring hit zone, it can be hit by the player
        if (other.gameObject == targetObject)
        {
            canBeHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the note exits the hit zone without being hit, it starts moving towards the miss zone
        if (other.gameObject == targetObject)
        {
            canBeHit = false;
            moveToMissZone = true;  // After leaving the hit zone, move to miss zone
        }
    }

    private bool CheckKeyPress()
    {
        // Check the corresponding key press based on the note's color
        switch (noteColor)
        {
            case "red":
                return Input.GetKey(KeyCode.A);
            case "blue":
                return Input.GetKey(KeyCode.E);
            case "green":
                return Input.GetKey(KeyCode.Space);
            default:
                return false;
        }
    }

    private void HitNote()
    {
        // Note hit successfully, destroy the note
        Destroy(gameObject);
        Debug.Log(noteColor + " note hit successfully!");
    }

    private void MissNote()
    {
        // Note missed, destroy the note
        Destroy(gameObject);
        Debug.Log(noteColor + " note missed!");
    }
}
