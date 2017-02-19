using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace Frobnicator.ViewModels
{
    public class PlaybackViewModel
    {
        private readonly AudioOutput.IPlaybackDevice playback;
        private readonly MidiInput.IMidiInput midiIn;

        public PlaybackViewModel(AudioOutput.IPlaybackDevice playback, MidiInput.IMidiInput midiIn)
        {
            this.playback = playback;
            this.midiIn = midiIn;

            StartCommand = ReactiveCommand.Create(() => { playback.Start(); midiIn.Start(); },
                playback.PlayState.Select(x => x != AudioOutput.PlayState.Playing));
            StopCommand = ReactiveCommand.Create(() => { playback.Stop(); midiIn.Stop(); },
                playback.PlayState.Select(x => x == AudioOutput.PlayState.Playing));

            playback.Stop();
        }

        public ICommand StartCommand { get; private set; }

        public ICommand StopCommand { get; private set; }
    }
}