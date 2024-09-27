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
    [Range(0.1f,10f)]
    public float noteDelay;
    int currentNote;

    bool startTime = false;
    bool delayDone = false;
    public static float timer;
    float timePlayed;
    float avgNoteLength;

    public void Setup(){
        float num = 0f;
        foreach(var note in notes){
            num+=note.Length;
        }
        avgNoteLength = num/notes.Count;

        for(int i = 0; i < notes.Count; i++){
            Transform targ = null;
            int targID = 0;
            GameObject obj = Instantiate(test, Vector3.zero, Quaternion.identity);
            if(notes[i].Length <= avgNoteLength){
                targ = targets[0];
                obj.GetComponent<SpriteRenderer>().color = Color.blue;
            }else if(notes[i].Length > avgNoteLength && notes[i].Length < avgNoteLength*1.5f){
                targ = targets[1];
                targID = 1;
                obj.GetComponent<SpriteRenderer>().color = Color.red;
            }else if(notes[i].Length >= avgNoteLength*1.5f){
                targ = targets[2];
                targID = 2;
                obj.GetComponent<SpriteRenderer>().color = Color.green;
            }
            obj.GetComponent<NoteGraphic>().InitializeOnSpawn(notes[i], this, targ, targID, noteDelay);
            startTime = true;
        }
    }

    public void StartSong(){
        Debug.Log("Start");
        source.Play();

        Debug.Log(notes[notes.Count-1].StartTime);
        Debug.Log(source.clip.length);
    }

    private void Update(){

        if(Input.GetKeyDown(KeyCode.K)){
            noteDelay/=2;
        }

        if(startTime){
            timer+=Time.deltaTime;
            
            if(timer >= delay && !delayDone){
                StartSong();
                delayDone = true;
            }
        }
    }
}