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

type PlaybackDevice(deviceNumber, sampleRate) =
    let waveOut = new WaveOutEvent()

    member this.start () =
        let provider = getWaveSeqProvider sampleRate (Streams.sine sampleRate 440.0)    
        waveOut.DeviceNumber <- deviceNumber
        waveOut.Init(provider)
        waveOut.Play()
        
    member this.stop () =
        waveOut.Stop()
    
    member this.pause () =
        waveOut.Pause()

    member this.isPlaying () =
        if waveOut <> null then
            waveOut.PlaybackState = PlaybackState.Playing
        else
            false
    
    
