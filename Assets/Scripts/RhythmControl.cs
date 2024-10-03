using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RhythmControl : MonoBehaviour
{
    public static List<NoteGraphic> activeNotes = new List<NoteGraphic>();
    public static TrackKey[] keyLists = new TrackKey[6];
    public TextMeshProUGUI[] keyText = new TextMeshProUGUI[6];

    public static float tolerance = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < keyText.Length; i++){
            keyText[i].text = Data.keys[i].ToString();
        }
    }

    public static void SetNotes(){

        if(keyLists[0] == null){
            if(Data.keys.Count == 0)
                Data.DefaultBinds();
            for(int i = 0; i < keyLists.Length; i++){
                keyLists[i] = new TrackKey(i);
                keyLists[i].SetKey(Data.keys[i]);
            }
        }

        foreach(var note in activeNotes){
            switch(note.target.gameObject.name){
                case "1":
                    keyLists[0].notesInKey.Add(note);
                    break;
                case "2":
                    keyLists[1].notesInKey.Add(note);
                    break;
                case "3":
                    keyLists[2].notesInKey.Add(note);
                    break;
                case "4":
                    keyLists[3].notesInKey.Add(note);
                    break;
                case "5":
                    keyLists[4].notesInKey.Add(note);
                    break;
                case "6":
                    keyLists[5].notesInKey.Add(note);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(keyLists[0] == null){
            return; 
        }
        
        for(int key = 0; key < 6; key++){
            if(Input.GetKeyDown(keyLists[key].code)){
                keyLists[key].down = true;
                foreach(var note in keyLists[key].notesInKey){
                    if(note != null && note.spawned && note.CheckNoteDist() <= tolerance){
                        note.Hit();
                    }
                }
            }else if(Input.GetKeyUp(keyLists[key].code)){
                keyLists[key].down = false;
                foreach(var note in keyLists[key].notesInKey){
                    if(note != null && note.CheckFollowing() && note.type == NoteType.Length && note.spawned && note.CheckChildDist() <= tolerance){
                        note.Hit();
                    }else if(note != null && note.CheckFollowing() && note.type == NoteType.Length && note.spawned){
                        note.FailedFollowingNote();
                        note.SetRenderer(true);
                    }
                }
            }
        }
    }
}

public class TrackKey{
    public int id;
    public List<NoteGraphic> notesInKey = new List<NoteGraphic>();
    public KeyCode code;
    public bool down;

    public TrackKey(int _id){
        id = _id;
    }

    public void SetKey(KeyCode _code){
        code = _code;
    }
}
