using Frobnicator.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace Frobnicator.Tests.ViewModels
{
    [TestFixture]
    public class PlaybackViewModelTests
    {
        [Test]
        public void StartCommandCanExecuteCallsIsRunning()
        {
            var device = Substitute.For<Playback.IPlaybackDevice>();
            var vm =  new PlaybackViewModel(device);

            vm.StartCommand.CanExecute(null);
            device.Received().IsPlaying();
        }

        [Test]
        public void StopCommandCanExecuteCallsIsRunning()
        {
            var device = Substitute.For<Playback.IPlaybackDevice>();
            var vm = new PlaybackViewModel(device);

            vm.StopCommand.CanExecute(null);
            device.Received().IsPlaying();
        }

        [Test]
        public void StartCommandCallsStart()
        {
            var device = Substitute.For<Playback.IPlaybackDevice>();
            var vm = new PlaybackViewModel(device);

            vm.StartCommand.Execute(null);
            device.Received().Start();
        }

        [Test]
        public void StopCommandCallsStop()
        {
            var device = Substitute.For<Playback.IPlaybackDevice>();
            device.IsPlaying().Returns(true);
            var vm = new PlaybackViewModel(device);
            
            vm.StopCommand.Execute(null);
            device.Received().Stop();
        }
    }
}