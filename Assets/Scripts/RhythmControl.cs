using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmControl : MonoBehaviour
{
    public static List<NoteGraphic> activeNotes = new List<NoteGraphic>();
    public static TrackKey[] keyLists = new TrackKey[3];
    AudioManager manager;

    public static float tolerance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<AudioManager>();
    }

    public static void UpdateNotes(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            CheckNotes(TrackType.A);
        }else if(Input.GetKeyDown(KeyCode.E)){
            CheckNotes(TrackType.E);
        }else if(Input.GetKeyDown(KeyCode.Space)){
            CheckNotes(TrackType.Space);
        }
    }

    void CheckNotes(TrackType trackNum){
        
    }
}

public class TrackKey{
    public static List<NoteGraphic> notesInKey = new List<NoteGraphic>();
}
