module MidiInput

open NAudio.Midi
open System
type IMidiInputs =
    abstract member DeviceNames : string array
    abstract member Manufacturers : string array
    abstract member ProductIds : int array

type MidiInputs() = 
    let deviceInfo =
        let deviceCount = MidiIn.NumberOfDevices
        match deviceCount with 
        | 0 -> []
        | _ -> [0..(deviceCount-1)] |> List.map MidiIn.DeviceInfo

    interface IMidiInputs with
        member this.DeviceNames with get() = deviceInfo |> List.map (fun info -> info.ProductName) |> List.toArray
        member this.Manufacturers with get() = deviceInfo |> List.map (fun info -> info.Manufacturer.ToString()) |> List.toArray
        member this.ProductIds with get() = deviceInfo |> List.map (fun info -> info.ProductId) |> List.toArray
