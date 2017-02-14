module MidiInput

open NAudio.Midi
open System

// Taken from http://newt.phys.unsw.edu.au/jw/notes.html
let public midiNoteToFrequency (noteNumber : int) : float =
    Math.Pow(2.0, (float (noteNumber - 69)) / 12.0) * 440.0

type IMidiInputs =
    abstract member DeviceNames : string array
    abstract member SelectedManufacturer : string
    abstract member SelectedProductId : int
    abstract member SelectedDevice : int with get, set

type MidiInputs() = 
    let deviceInfo =
        let deviceCount = MidiIn.NumberOfDevices
        match deviceCount with 
        | 0 -> []
        | _ -> [0..(deviceCount-1)] |> List.map MidiIn.DeviceInfo

    let mutable selectedDevice = 0

    interface IMidiInputs with
        member this.DeviceNames with get() = deviceInfo |> List.map (fun info -> info.ProductName) |> List.toArray
        member this.SelectedManufacturer with get() = deviceInfo.[selectedDevice].Manufacturer.ToString()
        member this.SelectedProductId with get() = deviceInfo.[selectedDevice].ProductId
        member this.SelectedDevice
            with get() = selectedDevice
            and set(value) = selectedDevice <- value