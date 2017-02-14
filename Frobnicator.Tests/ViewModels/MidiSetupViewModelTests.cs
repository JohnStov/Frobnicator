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
            var vm = new MidiSetupViewModel(inputs) {SelectedItem = 2};

            vm.SelectedItem.ShouldEqual(2);
        }

        [Test]
        public void DefaultSelectedItemIsFirst()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedItem.ShouldEqual(0);
        }

        [Test]
        public void DeviceNamesCalledMidiInputs()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.DeviceNames.Returns(new[] {"Device1", "Device2", "Device3"});
            var vm = new MidiSetupViewModel(inputs);

            vm.DeviceNames.ShouldEqual(new[] {"Device1", "Device2", "Device3"});
        }

        [Test]
        public void ItemSelectionIsPassedThrough()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);
            vm.SelectedItem = 2;
            inputs.Received().SelectedDevice = 2;
        }

        [Test]
        public void SelectedManufacturerIsUsed()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.SelectedManufacturer.Returns("Manufacturer1");
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedManufacturer.ShouldEqual("Manufacturer1");
        }

        [Test]
        public void SelectedProductIdIsUsed()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.SelectedProductId.Returns(100);
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedProductId.ShouldEqual("100");
        }

        [Test]
        public void SelectingAnItemFiresNotifyPropertyChanged()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);

            var triggerCount = 0;
            vm.PropertyChanged += (o, e) => ++triggerCount;

            vm.SelectedItem = 2;

            triggerCount.ShouldEqual(3);
        }
    }
}