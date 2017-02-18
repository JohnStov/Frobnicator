using System.Reactive.Subjects;
using Frobnicator.ViewModels;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace Frobnicator.Tests.ViewModels
{
    [TestFixture]
    public class MidiSetupViewModelTests
    {
        [Test]
        public void CanSelectAnItem()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input) {SelectedItem = 2};

            vm.SelectedItem.ShouldEqual(2);
        }

        [Test]
        public void DefaultSelectedItemIsFirst()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input);

            vm.SelectedItem.ShouldEqual(0);
        }

        [Test]
        public void DeviceNamesCalledMidiInputs()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            inputs.DeviceNames.Returns(new[] {"Device1", "Device2", "Device3"});
            var vm = new MidiSetupViewModel(inputs, input);

            vm.DeviceNames.ShouldEqual(new[] {"Device1", "Device2", "Device3"});
        }

        [Test]
        public void ItemSelectionIsPassedThrough()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input);
            vm.SelectedItem = 2;
            inputs.Received().SelectedDevice = 2;
        }

        [Test]
        public void SelectedManufacturerIsUsed()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.SelectedManufacturer.Returns("Manufacturer1");
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input);

            vm.SelectedManufacturer.ShouldEqual("Manufacturer1");
        }

        [Test]
        public void SelectedProductIdIsUsed()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.SelectedProductId.Returns(100);
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input);

            vm.SelectedProductId.ShouldEqual("100");
        }

        [Test]
        public void SelectingAnItemFiresNotifyPropertyChanged()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var vm = new MidiSetupViewModel(inputs, input);

            var triggerCount = 0;
            vm.PropertyChanged += (o, e) => ++triggerCount;

            vm.SelectedItem = 2;

            triggerCount.ShouldEqual(3);
        }

        [Test]
        public void IsDisabledWhenPlaying()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var subject = new Subject<AudioOutput.PlayState>();
            input.PlayState.Returns(subject);
            var vm = new MidiSetupViewModel(inputs, input);

            subject.OnNext(AudioOutput.PlayState.Playing);
            vm.IsEnabled.ShouldBeFalse();
        }

        [Test]
        public void IsEnabledWhenNotPlaying()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var input = Substitute.For<MidiInput.IMidiInput>();
            var subject = new Subject<AudioOutput.PlayState>();
            input.PlayState.Returns(subject);
            var vm = new MidiSetupViewModel(inputs, input);

            subject.OnNext(AudioOutput.PlayState.Stopped);
            vm.IsEnabled.ShouldBeTrue();
        }
    }
}