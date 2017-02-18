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
            var vm = new PlaybackViewModel(device);

            vm.StartCommand.Execute(null);
            device.Received().Start();
        }

        [Test]
        public void StopCommandCallsStop()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var vm = new PlaybackViewModel(device);

            vm.StopCommand.Execute(null);
            device.Received().Stop();
        }
    }
}