using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;
// ReSharper disable MemberCanBePrivate.Global

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

      public IEnumerable<string> Channels
         => new[] {"All", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"};

      private int selectedChannel;
      public int SelectedChannel
      {
         get { return selectedChannel+1; }
         set
         {
            selectedChannel = value-1;
            OnPropertyChanged(nameof(SelectedChannel));
         }
      }

      public string SelectedManufacturer => SelectedItem < 0 ? string.Empty : MidiInput.manufacturers[SelectedItem];

      public string SelectedProductId => SelectedItem < 0 ? string.Empty : MidiInput.productIds[SelectedItem].ToString();

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
