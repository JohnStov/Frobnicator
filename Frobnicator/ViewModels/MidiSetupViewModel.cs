using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;
using ReactiveUI;

namespace Frobnicator.ViewModels
{
    public class MidiSetupViewModel : ReactiveObject
    {
        private readonly MidiInput.IMidiInputs inputs;

        private int selectedChannel;

        public MidiSetupViewModel(MidiInput.IMidiInputs inputs)
        {
            this.inputs = inputs;
        }

        public IEnumerable<string> DeviceNames => inputs.DeviceNames;

        public int SelectedItem
        {
            get { return inputs.SelectedDevice; }
            set
            {
                inputs.SelectedDevice = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(SelectedManufacturer));
                this.RaisePropertyChanged(nameof(SelectedProductId));
            }
        }

        public IEnumerable<string> Channels
            => new[] {"All", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"};

        public int SelectedChannel
        {
            get { return selectedChannel + 1; }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedChannel, value - 1);
            }
        }

        public string SelectedManufacturer => SelectedItem < 0 ? string.Empty : inputs.SelectedManufacturer;

        public string SelectedProductId => SelectedItem < 0 ? string.Empty : inputs.SelectedProductId.ToString();
    }
}