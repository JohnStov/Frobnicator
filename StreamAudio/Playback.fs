module Playback

open System
open NAudio.Wave

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

type IPlaybackDevice =
    abstract member Start : unit-> unit
    abstract member Stop : unit-> unit
    abstract member Pause : unit-> unit
    abstract member IsPlaying : unit-> bool

type PlaybackDevice(deviceNumber, sampleRate) =
    let waveOut = new WaveOutEvent()

    interface IPlaybackDevice with
        member this.Start () =
            let provider = getWaveSeqProvider sampleRate (Streams.sine sampleRate 440.0)    
            waveOut.DeviceNumber <- deviceNumber
            waveOut.Init(provider)
            waveOut.Play()
        
        member this.Stop () =
            waveOut.Stop()
    
        member this.Pause () =
            waveOut.Pause()

        member this.IsPlaying () =
            if waveOut <> null then
                waveOut.PlaybackState = PlaybackState.Playing
            else
                false
    
    
