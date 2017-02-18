using System.Reactive.Subjects;
using Frobnicator.ViewModels;
using NSubstitute;
using NUnit.Framework;

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
            var vm = new PlaybackViewModel(device, midi);

            vm.StartCommand.Execute(null);
            device.Received().Start();
            midi.Received().Start();
        }

        [Test]
        public void StopCommandCallsStop()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var midi = Substitute.For<MidiInput.IMidiInput>();
            var vm = new PlaybackViewModel(device, midi);

            vm.StopCommand.Execute(null);
            device.Received().Stop();
            midi.Received().Stop();
        }
    }
}