using Frobnicator.ViewModels;
using NUnit.Framework;
using NSubstitute;
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
          devices.DeviceNames.Returns(new [] {"Device1", "Device2", "Device3"});
          var vm = new AudioSetupViewModel(devices);

          vm.DeviceNames.ShouldEqual(new[] { "Device1", "Device2", "Device3" });
       }

      [Test]
      public void SelectedItemDefaultsToZero()
      {
         var devices = Substitute.For<AudioOutput.IAudioDevices>();
         var vm = new AudioSetupViewModel(devices);

         vm.SelectedItem.ShouldEqual(0);
      }

      [Test]
      public void CanSelectItem()
      {
         var devices = Substitute.For<AudioOutput.IAudioDevices>();
         var vm = new AudioSetupViewModel(devices);

         vm.SelectedItem = 3;
         vm.SelectedItem.ShouldEqual(3);
      }

      [Test]
      public void SelectingItemFirePropertyChanged()
      {
         var devices = Substitute.For<AudioOutput.IAudioDevices>();
         var vm = new AudioSetupViewModel(devices);

         var propertyName = string.Empty;
         vm.PropertyChanged += (_, e) => propertyName = e.PropertyName;

         vm.SelectedItem = 2;

         propertyName.ShouldEqual("SelectedItem");
      }
   }
}
