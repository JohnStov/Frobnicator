using Frobnicator.ViewModels;
using NAudio.Midi;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Frobnicator.Tests.ViewModels
{
    [TestFixture]
    public class PlaybackViewModelTests
    {
        [Test]
        public void StartCommandCallsStart()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var midi = Substitute.For<MidiInput.IMidiInput>();
            midi.MidiData.Returns(Substitute.For<IObservable<MidiEvent>>());
            var vm = new PlaybackViewModel(device, midi);

            vm.StartCommand.Execute(null);
            device.Received().Start(midi.MidiData);
            midi.Received().Start();
        }

        [Test]
        public void StopCommandCallsStop()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var midi = Substitute.For<MidiInput.IMidiInput>();
            midi.MidiData.Returns(Substitute.For<IObservable<MidiEvent>>());
            var vm = new PlaybackViewModel(device, midi);

            vm.StopCommand.Execute(null);
            device.Received().Stop();
            midi.Received().Stop();
        }
    }
}