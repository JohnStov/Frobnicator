module MidiInput

open NAudio.Midi
open System

let deviceInfo =
    let deviceCount = MidiIn.NumberOfDevices
    match deviceCount with 
    | 0 -> []
    | _ -> [0..(deviceCount-1)] |> List.map MidiIn.DeviceInfo

let deviceNames =
    deviceInfo |> List.map (fun info -> info.ProductName)

let manufacturers =
    deviceInfo |> List.map (fun info -> info.Manufacturer.ToString())

let productIds =
    deviceInfo |> List.map (fun info -> info.ProductId)
