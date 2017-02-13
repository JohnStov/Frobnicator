module AudioOutput

open NAudio.Wave
open System 

type IAudioDevices =
    abstract member DeviceNames : string array

type AudioDevices () =
    let deviceInfo =
        let deviceCount = WaveOut.DeviceCount
        match deviceCount with 
        | 0 -> []
        | _ -> [0..(deviceCount-1)] |> List.map WaveOut.GetCapabilities

    interface IAudioDevices with
        member this.DeviceNames with get() = deviceInfo |> List.map (fun info -> info.ProductName) |> List.toArray
