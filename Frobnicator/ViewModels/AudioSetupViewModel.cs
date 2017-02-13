using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;

namespace Frobnicator.ViewModels
{
   public class AudioSetupViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      private readonly AudioOutput.IAudioDevices devices;

      public AudioSetupViewModel(AudioOutput.IAudioDevices devices)
      {
         this.devices = devices;
      }

      public IEnumerable<string> DeviceNames => devices.DeviceNames;

      private int selectedItem;

      public int SelectedItem
      {
         get { return selectedItem; }
         set
         {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
         }
      }

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}