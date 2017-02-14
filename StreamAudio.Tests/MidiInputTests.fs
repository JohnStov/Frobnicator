module MidiInputTests

open NUnit.Framework


[<TestFixture>]
type MidiInputTests() = 

    [<Test>]
    member this.``midiNoteToFrequency calculates correct frequency for MIDI notes``() =
        Assert.AreEqual(440.0, (MidiInput.midiNoteToFrequency 69), 0.01)
        Assert.AreEqual(415.3, (MidiInput.midiNoteToFrequency 68), 0.01)
        Assert.AreEqual(466.16, (MidiInput.midiNoteToFrequency 70), 0.01)
        Assert.AreEqual(4186.0, (MidiInput.midiNoteToFrequency 108), 0.01)
        Assert.AreEqual(27.5, (MidiInput.midiNoteToFrequency 21), 0.01)
        
 