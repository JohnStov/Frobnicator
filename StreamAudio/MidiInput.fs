module MidiInput

open NAudio.Midi
open System
open System.Reactive.Subjects

// Taken from http://newt.phys.unsw.edu.au/jw/notes.html
let public midiNoteToFrequency (noteNumber : int) : float =
    Math.Pow(2.0, (float (noteNumber - 69)) / 12.0) * 440.0

type IMidiInputs =
    abstract member DeviceNames : string array
    abstract member SelectedManufacturer : string
    abstract member SelectedProductId : int
    abstract member SelectedDevice : int with get, set

type IMidiInput =
    abstract member Start : unit -> unit
    abstract member Stop : unit -> unit
    abstract member PlayState : IObservable<AudioOutput.PlayState>
    abstract member MidiData : IObservable<MidiEvent>

type MidiInput() = 
    let deviceInfo =
        let deviceCount = MidiIn.NumberOfDevices
        match deviceCount with 
        | 0 -> []
        | _ -> [0..(deviceCount-1)] |> List.map MidiIn.DeviceInfo

    let mutable selectedDevice = 0

    let playState = new BehaviorSubject<AudioOutput.PlayState> (AudioOutput.PlayState.Stopped)

    let mutable midiIn : MidiIn = null;

    interface IMidiInputs with
        member this.DeviceNames with get() = deviceInfo |> List.map (fun info -> info.ProductName) |> List.toArray
        member this.SelectedManufacturer with get() = deviceInfo.[selectedDevice].Manufacturer.ToString()
        member this.SelectedProductId with get() = deviceInfo.[selectedDevice].ProductId
        member this.SelectedDevice
            with get() = selectedDevice
            and set(value) = selectedDevice <- value

    interface IMidiInput with
        member this.Start () = 
            if midiIn <> null then midiIn.Dispose()
            midiIn <- new MidiIn(selectedDevice)
            midiIn.Start()
            playState.OnNext AudioOutput.PlayState.Playing

        member this.Stop () =
            if midiIn <> null then midiIn.Stop()
            playState.OnNext AudioOutput.PlayState.Stopped
        
        member this.PlayState = playState :> IObservable<AudioOutput.PlayState>

        member this.MidiData = midiIn.MessageReceived |> Observable.map(fun x -> x.MidiEvent)

