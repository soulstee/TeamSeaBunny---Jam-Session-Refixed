using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static List<MidiNote> notes = new List<MidiNote>();
    public TextMeshProUGUI text;
    public GameObject test;
    public Transform[] targets;
    public AudioSource source;
    [HideInInspector]public float delay;
    [Range(0.01f,10f)]
    public float noteDelay;
    int currentNote;

    bool startTime = false;
    bool delayDone = false;
    public static float timer;
    float timePlayed;
    float avgNoteLength;

    private void Awake(){

        delay = noteDelay+3f;
    }

    public void Setup(){
        float num = 0f;
        foreach(var note in notes){
            num+=note.Length;
        }
        avgNoteLength = num/notes.Count;

        for(int i = 0; i < notes.Count; i++){
            int pos = (int)Random.Range(0, targets.Length);
            Transform targ = targets[pos];
            GameObject obj = Instantiate(test, Vector3.zero, Quaternion.identity);
        if(notes[i].Length >= avgNoteLength*2f){
            //obj.GetComponent<SpriteRenderer>().color = Color.green;
            obj.GetComponent<NoteGraphic>().InitializeOnSpawn(notes[i], this, targ, noteDelay, NoteType.Length);
        }else{
            //obj.GetComponent<SpriteRenderer>().color = Color.blue;
            obj.GetComponent<NoteGraphic>().InitializeOnSpawn(notes[i], this, targ, noteDelay, NoteType.Normal);
        }
        startTime = true;
        }
        
        RhythmControl.SetNotes();
    }

    public void StartSong(){
        source.Play();
    }

    private void Update(){

        if(timer >= delay && !delayDone){
            StartSong();
            delayDone = true;
        }

        if(startTime){
            timer+=Time.deltaTime;

            if((delay-noteDelay-timer) > 0){
                text.text = (Mathf.Round((delay-noteDelay-timer)*10f)*0.1f).ToString();
            }else{
                text.text = "";
            }
            
            if(timer >= delay && !delayDone){
                StartSong();
            }
        }
    }
}