using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace Frobnicator.ViewModels
{
    public class PlaybackViewModel
    {
        private readonly AudioOutput.IPlaybackDevice playback;

        public PlaybackViewModel(AudioOutput.IPlaybackDevice playback)
        {
            this.playback = playback;

            StartCommand = ReactiveCommand.Create(playback.Start,
                playback.PlayState.Select(x => x != AudioOutput.PlayState.Playing));
            StopCommand = ReactiveCommand.Create(playback.Stop,
                playback.PlayState.Select(x => x == AudioOutput.PlayState.Playing));

            playback.Stop();
        }

        public ICommand StartCommand { get; private set; }

        public ICommand StopCommand { get; private set; }
    }
}