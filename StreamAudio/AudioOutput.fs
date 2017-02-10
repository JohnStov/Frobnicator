module AudioOutput

open NAudio.Wave
open System

let deviceInfo =
    let deviceCount = WaveOut.DeviceCount
    match deviceCount with 
    | 0 -> []
    | _ -> [0..(deviceCount-1)] |> List.map WaveOut.GetCapabilities

let deviceNames =
    deviceInfo |> List.map (fun info -> info.ProductName)
