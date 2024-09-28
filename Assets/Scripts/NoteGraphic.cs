using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGraphic : MonoBehaviour
{
    public TrackType track;
    public float speed = 4f;
    private float noteDelay;
    private float length;
    private float angle;
    public float dist;
    public float vel;
    Vector2 movement;
    Rigidbody2D rb;
    SpriteRenderer renderer;
    public Transform target;
    public float timer = 0;
    bool missed = false;

    void Awake(){
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeOnSpawn(float _length, Transform targ, int _track, float _noteDelay){
        track = (TrackType)_track;
        RhythmControl.activeNotes.Add(this);
        RhythmControl.UpdateNotes();
        length = _length;
        target = targ;
        noteDelay = _noteDelay;
        SetMovement();
    }

    private void SetMovement(){
        dist = Vector2.Distance(transform.position, target.position);
        vel = dist/(length+noteDelay);

        movement = new Vector2(target.position.x, target.position.y);
    }

    private void FixedUpdate(){
        timer += Time.deltaTime;
        transform.Translate(movement * vel * Time.deltaTime);

        if(!missed && vel*timer >= dist+RhythmControl.tolerance){
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
