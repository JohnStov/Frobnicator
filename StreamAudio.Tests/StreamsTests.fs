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
