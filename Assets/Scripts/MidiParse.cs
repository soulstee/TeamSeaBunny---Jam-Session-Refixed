using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Common;

public class MidiParse : MonoBehaviour
{
    public static List<MidiNote> notes = new List<MidiNote>();
    [SerializeField] private string midiFilePath;
    [SerializeField] private float bpm = 120f; //Default bpm on MIDI

    AudioManager manager;

    void Awake()
    {
        manager = GetComponent<AudioManager>();
        ParseMidiFile(Path.Combine(Application.streamingAssetsPath, midiFilePath));
    }

    private void ParseMidiFile(string path)
    {
        MidiFile midiFile = MidiFile.Read(path + ".mid");
        Dictionary<int, float> noteStartTimes = new Dictionary<int, float>();

        float ticksPerBeat = 480f; //MIDI file default TPB
        float secondsPerBeat = bpm / 60; 
        float secondsPerTick = secondsPerBeat / ticksPerBeat;

        foreach (var trackChunk in midiFile.GetTrackChunks())
        {
            float currentTime = 0f;

            foreach (var midiEvent in trackChunk.Events)
            {
                currentTime += midiEvent.DeltaTime * secondsPerTick; // Convert ticks to seconds

                if (midiEvent.EventType == MidiEventType.NoteOn)
                {
                    var noteOnEvent = (NoteOnEvent)midiEvent;
                    
                    noteStartTimes[noteOnEvent.NoteNumber] = currentTime;

                    notes.Add(new MidiNote
                    {
                        NoteNumber = noteOnEvent.NoteNumber,
                        Velocity = noteOnEvent.Velocity,
                        StartTime = currentTime,
                        Length = 0f,
                    });
                }
                else if (midiEvent.EventType == MidiEventType.NoteOff)
                {
                    var noteOffEvent = (NoteOffEvent)midiEvent;
                    
                    if (noteStartTimes.TryGetValue(noteOffEvent.NoteNumber, out float startTime))
                    {
                        var note = notes.Find(n => n.NoteNumber == noteOffEvent.NoteNumber && n.Length == 0f);
                        if (note != null)
                        {
                            note.Length = currentTime - startTime; //Calculate length between note on and off
                        }
                        
                        noteStartTimes.Remove(noteOffEvent.NoteNumber);
                    }
                }
            }
        }

        AudioManager.notes = notes;
        manager.Setup();
    }
}

[System.Serializable]
public class MidiNote
    {
        public int NoteNumber { get; set; }
        public int Velocity { get; set; }
        public float StartTime { get; set; }
        public float Length { get; set; }

        public bool active = false;
        public bool played = false;
    }
