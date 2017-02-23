using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Frobnicator.ViewModels
{
    public class PlaybackViewModel
    {
        public PlaybackViewModel(AudioOutput.IPlaybackDevice playback, MidiInput.IMidiInput midiIn)
        {
            StartCommand = ReactiveCommand.Create(() => { playback.Start(midiIn.MidiData); midiIn.Start(); },
                playback.PlayState.Select(x => x != AudioOutput.PlayState.Playing));
            StopCommand = ReactiveCommand.Create(() => { playback.Stop(); midiIn.Stop(); },
                playback.PlayState.Select(x => x == AudioOutput.PlayState.Playing));

            playback.Stop();
        }

        public ICommand StartCommand { get; private set; }

        public ICommand StopCommand { get; private set; }
    }
}