module Streams

open NAudio.Midi
open System
open System.Collections.Generic
open System.Diagnostics

let TwoPi = 2.0 * Math.PI

type Stream = float seq

let sine sampleRate freq = 
    let rec gen theta =
        seq {
            let delta = TwoPi * freq / sampleRate
            yield Math.Sin theta
            let newTheta = (theta + delta) % TwoPi 
            yield! gen newTheta
        }
    gen 0.0

let valueStream (value : float) : Stream =
    let rec generate () =
        seq {
            yield value
            yield! generate ()
        }
    generate ()

let sineStream sampleRate (freq : Stream) : Stream = 
    let enumerator = freq.GetEnumerator()
    let rec gen theta =
        seq {
            let delta = TwoPi * enumerator.Current  / sampleRate
            yield Math.Sin theta
            enumerator.MoveNext() |> ignore
            let newTheta = (theta + delta) % TwoPi 
            yield! gen newTheta
        }
    gen 0.0

// Taken from http://newt.phys.unsw.edu.au/jw/notes.html
let midiNoteToFrequency (noteNumber : int) : float =
    Math.Pow(2.0, (float (noteNumber - 69)) / 12.0) * 440.0

let noteFreqStream (notes : IObservable<MidiEvent>) =
    let mutable currentFrequency = 0.0
    let mutable currentNotes : NoteEvent list = []
    
    let newNoteHandler (note : NoteEvent) notes : NoteEvent list =
        match note.CommandCode, note.Velocity with
        | MidiCommandCode.NoteOn, 0
        | MidiCommandCode.NoteOff, _ -> 
                notes |> List.filter(fun x -> x.NoteNumber <> note.NoteNumber)
        | MidiCommandCode.NoteOn, _ -> 
            if notes |> List.exists(fun x -> x.NoteNumber = note.NoteNumber)
            then notes
            else note :: notes
        | _ , _ -> notes

    let onNewNote (note : NoteEvent) =
        Debug.Write currentNotes 
        currentNotes <- newNoteHandler note currentNotes

        currentFrequency <- match currentNotes with
                            | h :: _ -> midiNoteToFrequency h.NoteNumber
                            | [] -> 0.0

        Debug.Write " -> "
        Debug.WriteLine currentNotes
        
    notes 
        |> Observable.map(fun x -> x :?> NoteEvent)
        |> Observable.filter(fun x -> x <> null)
        |> Observable.subscribe(onNewNote) |> ignore

    let rec gen () =
        seq {
            yield currentFrequency
            yield! gen ()
        }
    gen ()

            
            
    
    