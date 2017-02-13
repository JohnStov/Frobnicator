using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Frobnicator.Annotations;

namespace Frobnicator.ViewModels
{
    public class AudioSetupViewModel : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public bool IsEnabled => !device.IsPlaying();
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPlayStateChanged(object obj0, EventArgs obj1)
        {
            OnPropertyChanged(nameof(IsEnabled));
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}