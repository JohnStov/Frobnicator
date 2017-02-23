namespace StreamAudio.Tests

open NUnit.Framework

[<TestFixture>]
type StreamsTests() = 

    [<Test>]
    member this.``sine creates a time-varying sequence``() =
        let sine = Streams.sine 44100.0 440.0
        let pairs = sine |> Seq.pairwise |> Seq.take 100
        Assert.IsTrue(pairs |> Seq.forall (fun (x, y) -> x <> y))
    
    [<Test>]
    member this.``maximum sine amplitude is 1.0``() =
        let sine = Streams.sine 44100.0 440.0 |> Seq.take 1000
        Assert.IsTrue(sine |> Seq.forall (fun x -> x <= 1.0))

    [<Test>]
    member this.``minimum sine amplitude is -1.0``() =
        let sine = Streams.sine 44100.0 440.0 |> Seq.take 1000
        Assert.IsTrue(sine |> Seq.forall (fun x -> x >= -1.0))

    [<Test>]
    member this.``valueStream repeats value``() =
        let strm = Streams.valueStream 250.0 |> Seq.take 100
        Assert.IsTrue(strm |> Seq.forall (fun x -> x = 250.0))

    [<Test>]
    member this.``sineStream produces different values for different frequencies``() =
        let strm1 = Streams.valueStream 330.0 |> Streams.sineStream 44100.0 |> Seq.skip 10 |> Seq.take 100
        let strm2 = Streams.valueStream 440.0 |> Streams.sineStream 44100.0 |> Seq.skip 10 |> Seq.take 100
        Assert.IsTrue(Seq.zip strm1 strm2 |> Seq.forall (fun (x, y) -> x <> y))

    [<Test>]
    member this.``midiNoteToFrequency calculates correct frequency for MIDI notes``() =
        Assert.AreEqual(440.0, (Streams.midiNoteToFrequency 69), 0.01)
        Assert.AreEqual(415.3, (Streams.midiNoteToFrequency 68), 0.01)
        Assert.AreEqual(466.16, (Streams.midiNoteToFrequency 70), 0.01)
        Assert.AreEqual(4186.0, (Streams.midiNoteToFrequency 108), 0.01)
        Assert.AreEqual(27.5, (Streams.midiNoteToFrequency 21), 0.01)



