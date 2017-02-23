module AudioOutput

open NAudio.Wave
open System
open System.Reactive.Subjects

let getWaveSeqProvider (sampleRate : float) (stream : float seq) =
    let channelCount = 1

    let readStream (buffer : byte []) offset count =
        let sampleCount = count / sizeof<float32>
        let samples = stream |> Seq.map (fun x -> BitConverter.GetBytes((float32)x)) |> Seq.take sampleCount |> Array.concat
        Array.blit samples 0 buffer offset count
        count

    { new IWaveProvider with
      member p.Read(buffer : byte [], offset, count) =
          readStream buffer offset count
             
      member p.WaveFormat = 
          WaveFormat.CreateIeeeFloatWaveFormat((int)sampleRate, channelCount) 
    }

type PlayState = Stopped = 0 | Paused = 1 | Playing  = 2

type IAudioDevices =
    abstract member DeviceNames : string array
    abstract member SelectedDevice : int with get, set

type IPlaybackDevice =
    abstract member Start : unit-> unit
    abstract member Stop : unit-> unit
    abstract member Pause : unit-> unit
    abstract member PlayState : IObservable<PlayState>

type AudioOutput (sampleRate) =
    let deviceInfo =
        let deviceCount = WaveOut.DeviceCount
        match deviceCount with 
        | 0 -> []
        | _ -> [0..(deviceCount-1)] |> List.map WaveOut.GetCapabilities

    let waveOut = new WaveOutEvent()
    let mutable selectedDevice = 0
    
    let playState = new BehaviorSubject<PlayState>(PlayState.Stopped)

    interface IAudioDevices with
        member this.DeviceNames = 
            deviceInfo |> List.map (fun info -> info.ProductName) |> List.toArray
       
        member this.SelectedDevice 
            with get() = selectedDevice
            and set(value) = selectedDevice <- value
            
    interface IPlaybackDevice with
        member this.Start () =
            let provider = getWaveSeqProvider sampleRate (Streams.sine sampleRate 440.0)    
            waveOut.DeviceNumber <- selectedDevice
            waveOut.Init(provider)
            waveOut.Play()
            playState.OnNext PlayState.Playing
        
        member this.Stop () =
            waveOut.Stop()
            playState.OnNext PlayState.Stopped
    
        member this.Pause () =
            waveOut.Pause()
            playState.OnNext PlayState.Paused

        member this.PlayState = playState :> IObservable<PlayState>

