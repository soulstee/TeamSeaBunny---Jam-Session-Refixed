using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject redNotePrefab;
    public GameObject blueNotePrefab;
    public GameObject greenNotePrefab;

    public Transform spawnPoint;
    public GameObject targetA;
    public GameObject targetE;
    public GameObject targetSpace;
    
    public GameObject missZoneA;  // New miss zone for red notes
    public GameObject missZoneE;  // New miss zone for blue notes
    public GameObject missZoneSpace;  // New miss zone for green notes

    public float bpm = 120f;  // Beats Per Minute of the song
    private float spawnInterval;
    private float timer;

    private void Start()
    {
        // Calculate the spawn interval based on the BPM
        spawnInterval = 60f / bpm;
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomNote();
            timer = spawnInterval;  // Reset the timer for the next spawn
        }
    }

    private void SpawnRandomNote()
    {
        int randomIndex = (int)Random.Range(0, 3);
        GameObject noteToSpawn = null;
        GameObject target = null;
        GameObject missZone = null;
        string noteColor = "";

        switch (randomIndex)
        {
            case 0:
                noteToSpawn = redNotePrefab;
                target = targetA;
                missZone = missZoneA;  // Set miss zone for red notes
                noteColor = "red";
                break;
            case 1:
                noteToSpawn = blueNotePrefab;
                target = targetE;
                missZone = missZoneE;  // Set miss zone for blue notes
                noteColor = "blue";
                break;
            case 2:
                noteToSpawn = greenNotePrefab;
                target = targetSpace;
                missZone = missZoneSpace;  // Set miss zone for green notes
                noteColor = "green";
                break;
        }

        if (noteToSpawn != null && target != null && missZone != null)
        {
            GameObject newNote = Instantiate(noteToSpawn, spawnPoint.position, Quaternion.identity);
            Note noteScript = newNote.GetComponent<Note>();

            // Pass target, color, and movement speed to the note
            noteScript.targetObject = target;
            noteScript.missZoneObject = missZone;  // Pass the miss zone to the note
            noteScript.noteColor = noteColor;
            noteScript.bpm = bpm;  // Set the BPM for note speed calculation
        }
    }
}
