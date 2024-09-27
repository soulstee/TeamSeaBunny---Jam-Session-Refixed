using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGraphic : MonoBehaviour
{
    private MidiNote note;
    private AudioManager audio;
    public TrackType track;
    public float speed = 4f;
    private float noteDelay;
    public float dist;
    public float vel;
    Vector2 movement;
    Rigidbody2D rb;
    SpriteRenderer renderer;
    public Transform target;
    bool missed = false;
    bool spawned = false;

    void Awake(){
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeOnSpawn(MidiNote _note, AudioManager _manager, Transform targ, int _track, float _noteDelay){
        audio = _manager;
        note = _note;
        track = (TrackType)_track;
        RhythmControl.activeNotes.Add(this);
        RhythmControl.UpdateNotes();
        target = targ;
        noteDelay = _noteDelay;
        SetMovement();
    }

    private void SetMovement(){
        dist = Vector2.Distance(transform.position, target.position);
        vel = dist/(noteDelay);

        movement = new Vector2(target.position.x, target.position.y).normalized;
    }

    private void Update(){
        if(!spawned && AudioManager.timer >= note.StartTime+audio.delay-noteDelay){
            spawned = true;
            renderer.enabled = true;
        }
        
        if(spawned){
            transform.Translate(movement * vel * Time.deltaTime);
        }

        if(!missed && vel*(audio.source.time - note.StartTime) >= dist+RhythmControl.tolerance){
            Miss();
        }
    }

    private void Destroy(float _time){
        RhythmControl.activeNotes.Remove(this);
        Destroy(this.gameObject, _time);
    }

    public void Hit(){
        Debug.Log("Hit");
        Destroy(0f);
    }

    public void Miss(){
        missed = true;
        Destroy(3f);
    }
}

public enum TrackType{
    E = 0,
    A = 1,
    Space = 2,

}
