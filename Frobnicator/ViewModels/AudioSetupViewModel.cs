using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;

namespace Frobnicator.ViewModels
{
   public class AudioSetupViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      public IEnumerable<string> DeviceNames => AudioOutput.deviceNames;

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
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}