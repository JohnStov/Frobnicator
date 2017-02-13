using System.Runtime.InteropServices;
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
       public void DeviceNamesCalledMidiInputs()
       {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.DeviceNames.Returns(new[] {"Device1", "Device2", "Device3"});
            var vm = new MidiSetupViewModel(inputs);
            
            vm.DeviceNames.ShouldEqual(new[] { "Device1", "Device2", "Device3" });
        }

       [Test]
       public void DefaultSelectedItemIsFirst()
       {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedItem.ShouldEqual(0);
        }

        [Test]
        public void CanSelectAnItem()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedItem = 2;
            vm.SelectedItem.ShouldEqual(2);
        }

        [Test]
        public void SelectingAnItemChangesSelectedManufacturer()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.DeviceNames.Returns(new[] { "Device1", "Device2", "Device3" });
            inputs.Manufacturers.Returns(new[] { "Manufacturer1", "Manufacture2", "Manufacturer3" });
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedItem = 2;
            vm.SelectedManufacturer.ShouldEqual("Manufacturer3");
        }

        [Test]
        public void SelectingAnItemChangesSelectedProductId()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            inputs.DeviceNames.Returns(new[] { "Device1", "Device2", "Device3" });
            inputs.ProductIds.Returns(new[] { 100, 101, 102 });
            var vm = new MidiSetupViewModel(inputs);

            vm.SelectedItem = 2;
            vm.SelectedProductId.ShouldEqual("102");
        }

        [Test]
        public void SelectingAnItemFiresNotifyPropertyChanged()
        {
            var inputs = Substitute.For<MidiInput.IMidiInputs>();
            var vm = new MidiSetupViewModel(inputs);

            int triggerCount = 0;
            vm.PropertyChanged += (o, e) => ++triggerCount;

            vm.SelectedItem = 2;

            triggerCount.ShouldEqual(3);
        }
    }
}