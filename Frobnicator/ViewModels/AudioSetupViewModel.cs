using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            device.PlayStateChanged += OnPlayStateChanged;
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

        public bool IsEnabled => !device.IsPlaying();

        private void OnPlayStateChanged(object obj0, EventArgs obj1)
        {
            this.RaisePropertyChanged(nameof(IsEnabled));
        }
    }
}