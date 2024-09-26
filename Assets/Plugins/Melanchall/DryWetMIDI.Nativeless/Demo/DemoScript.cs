using System;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
	private sealed class LogOutputDevice : IOutputDevice
	{
		public event EventHandler<MidiEventSentEventArgs> EventSent;

		public void PrepareForEventsSending()
		{
		}

		public void SendEvent(MidiEvent midiEvent)
		{
			Debug.Log($"Event sent: {midiEvent}");
		}

		public void Dispose()
		{
		}
	}

    private IOutputDevice _outputDevice;
    private Playback _playback;

    private void Start()
    {
        InitializeOutputDevice();
        var midiFile = CreateTestFile();
        InitializeFilePlayback(midiFile);
        StartPlayback();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Releasing playback and device...");

        if (_playback != null)
        {
            _playback.NotesPlaybackStarted -= OnNotesPlaybackStarted;
            _playback.NotesPlaybackFinished -= OnNotesPlaybackFinished;
            _playback.Dispose();
        }

        if (_outputDevice != null)
            _outputDevice.Dispose();

        Debug.Log("Playback and device released.");
    }

    private void InitializeOutputDevice()
    {
        Debug.Log($"Initializing output device...");
        _outputDevice = new LogOutputDevice();
        Debug.Log($"Output device initialized.");
    }

    private MidiFile CreateTestFile()
    {
        Debug.Log("Creating test MIDI file...");

        var patternBuilder = new PatternBuilder()
            .SetNoteLength(MusicalTimeSpan.Eighth)
            .SetVelocity(SevenBitNumber.MaxValue)
            .ProgramChange(GeneralMidiProgram.Harpsichord);

        foreach (var noteNumber in SevenBitNumber.Values)
        {
            patternBuilder.Note(Melanchall.DryWetMidi.MusicTheory.Note.Get(noteNumber));
        }

        var midiFile = patternBuilder.Build().ToFile(TempoMap.Default);
        Debug.Log("Test MIDI file created.");

        return midiFile;
    }

    private void InitializeFilePlayback(MidiFile midiFile)
    {
        Debug.Log("Initializing playback...");

        _playback = midiFile.GetPlayback(_outputDevice);
        _playback.Loop = true;
        _playback.NotesPlaybackStarted += OnNotesPlaybackStarted;
        _playback.NotesPlaybackFinished += OnNotesPlaybackFinished;
       
        Debug.Log("Playback initialized.");
    }

    private void StartPlayback()
    {
        Debug.Log("Starting playback...");
        _playback.Start();
    }

    private void OnNotesPlaybackFinished(object sender, NotesEventArgs e)
    {
        LogNotes("Notes finished:", e);
    }

    private void OnNotesPlaybackStarted(object sender, NotesEventArgs e)
    {
        LogNotes("Notes started:", e);
    }

    private void LogNotes(string title, NotesEventArgs e)
    {
        var message = new StringBuilder()
            .AppendLine(title)
            .AppendLine(string.Join(Environment.NewLine, e.Notes.Select(n => $"  {n}")))
            .ToString();
        Debug.Log(message.Trim());
    }
}