using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;

namespace Frobnicator.ViewModels
{
   public class MidiSetupViewModel : INotifyPropertyChanged
   {
      public IEnumerable<string> DeviceNames => MidiInput.deviceNames;

      private int selectedItem;

      public int SelectedItem
      {
         get { return selectedItem; }
         set
         {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(SelectedManufacturer));
            OnPropertyChanged(nameof(SelectedProductId));
         }
      }

      public string SelectedManufacturer
      {
         get { return SelectedItem < 0 ? string.Empty : MidiInput.manufacturers[SelectedItem]; }
      }

      public string SelectedProductId
      {
         get { return SelectedItem < 0 ? string.Empty : MidiInput.productIds[SelectedItem].ToString(); }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
