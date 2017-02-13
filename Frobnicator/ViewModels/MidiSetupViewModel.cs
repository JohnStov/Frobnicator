using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;

// ReSharper disable MemberCanBePrivate.Global

namespace Frobnicator.ViewModels
{
    public class MidiSetupViewModel : INotifyPropertyChanged
    {
        private readonly MidiInput.IMidiInputs inputs;

        private int selectedChannel;

        private int selectedItem;

        public MidiSetupViewModel(MidiInput.IMidiInputs inputs)
        {
            this.inputs = inputs;
        }

        public IEnumerable<string> DeviceNames => inputs.DeviceNames;

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

        public int SelectedChannel
        {
            get { return selectedChannel + 1; }
            set
            {
                selectedChannel = value - 1;
                OnPropertyChanged(nameof(SelectedChannel));
            }
        }

        public string SelectedManufacturer => SelectedItem < 0 ? string.Empty : inputs.Manufacturers[SelectedItem];

        public string SelectedProductId => SelectedItem < 0 ? string.Empty : inputs.ProductIds[SelectedItem].ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}