using Frobnicator.ViewModels;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace Frobnicator.Tests.ViewModels
{
    [TestFixture]
    public class AudioSetupViewModelTests
    {
        [Test]
        public void CanReadItems()
        {
            var devices = Substitute.For<AudioOutput.IAudioDevices>();
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            devices.DeviceNames.Returns(new[] {"Device1", "Device2", "Device3"});
            var vm = new AudioSetupViewModel(devices, device);

            vm.DeviceNames.ShouldEqual(new[] {"Device1", "Device2", "Device3"});
        }

        [Test]
        public void CanSelectItem()
        {
            var devices = Substitute.For<AudioOutput.IAudioDevices>();
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var vm = new AudioSetupViewModel(devices, device) {SelectedItem = 3};

            vm.SelectedItem.ShouldEqual(3);
        }

        [Test]
        public void IsDisabledWhenPlaying()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            device.IsPlaying().Returns(true);
            var vm = new AudioSetupViewModel(null, device);

            vm.IsEnabled.ShouldBeFalse();
        }

        [Test]
        public void IsEnabledWhenNotPlaying()
        {
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            device.IsPlaying().Returns(false);
            var vm = new AudioSetupViewModel(null, device);

            vm.IsEnabled.ShouldBeTrue();
        }

        [Test]
        public void SelectedItemDefaultsToZero()
        {
            var devices = Substitute.For<AudioOutput.IAudioDevices>();
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var vm = new AudioSetupViewModel(devices, device);

            vm.SelectedItem.ShouldEqual(0);
        }

        [Test]
        public void SelectingItemFirePropertyChanged()
        {
            var devices = Substitute.For<AudioOutput.IAudioDevices>();
            var device = Substitute.For<AudioOutput.IPlaybackDevice>();
            var vm = new AudioSetupViewModel(devices, device);

            var propertyName = string.Empty;
            vm.PropertyChanged += (_, e) => propertyName = e.PropertyName;

            vm.SelectedItem = 2;

            propertyName.ShouldEqual("SelectedItem");
        }
    }
}