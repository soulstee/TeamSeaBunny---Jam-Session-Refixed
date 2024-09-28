using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static List<MidiNote> notes = new List<MidiNote>();
    public GameObject test;
    public Transform[] targets;
    public AudioSource source;
    public float delay;
    public float noteDelay;
    int currentNote;

    bool startTime = false;
    bool delayDone = false;
    float timer;
    float timePlayed;
    float avgNoteLength;

    private void Start(){
        
    }

    public void StartSong(){
        timer = 0-delay;
        startTime = true;
        Debug.Log("Start");
        source.Play();
        float num = 0f;
        foreach(var note in notes){
            num+=note.Length;
        }
        avgNoteLength = num/notes.Count;//Avg length of all notes
    }

    private void Update(){

        if(startTime){
            timer+=Time.deltaTime;
            
            if(timer >= 0 && !delayDone){
                delayDone = true;
            }

            if(timer >= notes[currentNote].StartTime-noteDelay && !notes[currentNote].played && !notes[currentNote].active){
                    Transform targ = null;
                    int targID = 0;
                    notes[currentNote].active = true;
                    GameObject obj = Instantiate(test, Vector3.zero, Quaternion.identity);
                    if(notes[currentNote].Length <= avgNoteLength){
                        targ = targets[0];
                        obj.GetComponent<SpriteRenderer>().color = Color.blue;
                    }else if(notes[currentNote].Length > avgNoteLength && notes[currentNote].Length < avgNoteLength*1.5f){
                        targ = targets[1];
                        targID = 1;
                        obj.GetComponent<SpriteRenderer>().color = Color.red;
                    }else if(notes[currentNote].Length >= avgNoteLength*1.5f){
                        targ = targets[2];
                        targID = 2;
                        obj.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    obj.GetComponent<NoteGraphic>().InitializeOnSpawn(notes[currentNote].Length, targ, targID, noteDelay);
                    currentNote++;
            }
        }
    }
}