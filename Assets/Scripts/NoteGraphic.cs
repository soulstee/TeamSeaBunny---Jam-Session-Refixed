using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGraphic : MonoBehaviour
{
    private MidiNote note;
    private new AudioManager audio; // Hiding the inherited member
    public NoteType type = NoteType.Normal;
    [HideInInspector]
    public float dist;
    [HideInInspector]
    public float vel;
    Vector2 movement;
    private new SpriteRenderer renderer; // Hiding the inherited member

    [Header("Graphics Settings")]
    public Sprite[] spriteCycle;
    public float shakeAmount;
    public float shakeSpeed;

    [Header("Length Note")]
    public Sprite lengthNoteSprite;
    public Material lineMaterial;
    public Gradient lineColor;
    public float lineWidth;

    [Header("Prefabs")]
    public GameObject hitEffect;
    public Sprite[] spritesHit;

    [HideInInspector]
    public Transform target;
    bool missed = false;
    bool hit = false;
    [HideInInspector]
    public bool spawned = false;
    bool initialized = false;
    int key;
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    GameObject lengthChild;
    LineRenderer lineRenderer;
    bool following = false;
    bool failedFollow = false;

    public void InitializeOnSpawn(MidiNote _note, AudioManager _manager, Transform targ, float _noteDelay, NoteType _type)
    {
        audio = _manager;
        note = _note;
        RhythmControl.activeNotes.Add(this);
        target = targ;
        type = _type;

        if (type == NoteType.Length)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.colorGradient = lineColor;
            lineRenderer.widthMultiplier = lineWidth;
            lineRenderer.SetPosition(0, transform.position);
            renderer.sprite = lengthNoteSprite;
            lengthChild = Instantiate(gameObject, transform.position, transform.rotation);
            lengthChild.name = "LengthChild";
            if (lengthChild.GetComponent<SpriteRenderer>() == null)
                lengthChild.AddComponent<SpriteRenderer>().sprite = lengthNoteSprite;
            lengthChild.GetComponent<SpriteRenderer>().enabled = false;
        }
        initialized = true;
    }

    float childDist;

    private void Update()
    {
        if (initialized && !spawned && AudioManager.timer >= note.StartTime + audio.delay - audio.noteDelay)
        {
            dist = Vector2.Distance(transform.position, target.position);
            vel = dist / (audio.noteDelay);
            movement = new Vector2(target.position.x, target.position.y).normalized;
            spawned = true;
            renderer.enabled = true;

            if (type == NoteType.Length)
            {
                lengthChild.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (spawned && initialized)
        {

            if (!hit || failedFollow && renderer.enabled)
                transform.Translate(movement * vel * Time.deltaTime);

            if (type == NoteType.Length)
            {
                if (lengthChild != null && following || failedFollow)
                {
                    lengthChild.transform.Translate(movement * vel * Time.deltaTime);
                }
                else
                {
                    childDist = Vector2.Distance(transform.position, lengthChild.transform.position);
                    if (childDist > note.Length)
                    {
                        following = true;
                    }
                }

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, lengthChild.transform.position);
            }
        }

        if (spawned && !missed && vel * (audio.source.time - note.StartTime) > RhythmControl.tolerance+1f || Mathf.Abs(transform.position.x) > 75f)
        {
            Miss();
        }
    }

    public void SetKeyInt(int _key){
        key = _key;
    }

    private void Destroy(float _time)
    {
        Destroy(this.gameObject, _time);
    }

    public float CheckNoteDist()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    public bool CheckFollowing()
    {
        return following;
    }

    public bool CheckMissed()
    {
        return missed;
    }

    public void FailedFollowingNote()
    {
        renderer.enabled = true;
        failedFollow = true;
        Miss();
    }

    public float CheckChildDist()
    {
        return Vector2.Distance(transform.position, lengthChild.transform.position);
    }

    public void SetRenderer(bool set)
    {
        renderer.enabled = set;
    }

    public float TimeExisted()
    {
        return (audio.source.time - note.StartTime);
    }

    public void Hit(float tol, PlayerScore scoreScript)
    {
        if (type == NoteType.Normal)
        {
            RhythmControl.activeNotes.Remove(this);
            CheckPointAccuracy(tol, scoreScript);
            renderer.enabled = false;
            Destroy(0.0f);
        }
        else if (type == NoteType.Length && !hit && renderer.enabled)
        {
            hit = true;
            renderer.enabled = false;
        }
        else if (type == NoteType.Length && hit && !renderer.enabled)
        {
            RhythmControl.activeNotes.Remove(this);
            CheckPointAccuracy(tol, scoreScript);
            Destroy(lengthChild);
            Destroy(0.0f);
        }
    }

    public void CheckPointAccuracy(float tol, PlayerScore scoreScript){
        if(tol < 0.9 && tol > 0.5){
            scoreScript.SetScore(0);
            GameObject obj = Instantiate(hitEffect, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = spritesHit[3];
            Destroy(obj, 1f);
        }else if(tol <= 0.5 && tol >= 0.3){
            scoreScript.SetScore(25);
            GameObject obj = Instantiate(hitEffect, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = spritesHit[2];
            Destroy(obj, 1f);
        }else if(tol <= 0.3 && tol >= 0.15){
            scoreScript.SetScore(50);
            GameObject obj = Instantiate(hitEffect, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = spritesHit[1];
            Destroy(obj, 1f);
        }else if(tol < 0.15){
            scoreScript.SetScore(100);
            GameObject obj = Instantiate(hitEffect, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = spritesHit[0];
            Destroy(obj, 1f);
        }
    }

    public void Miss()
    {
        RhythmControl.activeNotes.Remove(this);
        Debug.Log("Missed");
        if (lengthChild != null)
        {
            Destroy(5f + note.Length);
            Destroy(lengthChild, 5f + note.Length);
            return;
        }
        Destroy(5f);

        missed = true;
    }
}

public enum NoteType
{

    Normal,
    Length,
}
