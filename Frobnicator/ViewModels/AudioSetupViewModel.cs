using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;
using ReactiveUI;

namespace Frobnicator.ViewModels
{
    public class AudioSetupViewModel : ReactiveObject
    {
        private readonly AudioOutput.IPlaybackDevice device;

        private readonly AudioOutput.IAudioDevices devices;

        public AudioSetupViewModel(AudioOutput.IAudioDevices devices, AudioOutput.IPlaybackDevice device)
        {
            this.devices = devices;
            this.device = device;

            isEnabled = device.PlayState.Select(x => x != AudioOutput.PlayState.Playing).ToProperty(this, x => x.IsEnabled);
        }

        public IEnumerable<string> DeviceNames => devices.DeviceNames;

        public int SelectedItem
        {
            get { return devices.SelectedDevice; }
            set
            {
                devices.SelectedDevice = value;
                this.RaisePropertyChanged();
            }
        }

        private ObservableAsPropertyHelper<bool> isEnabled;
        public bool IsEnabled => isEnabled.Value;
    }
}