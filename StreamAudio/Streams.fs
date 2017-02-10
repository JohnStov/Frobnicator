module Streams

open System

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