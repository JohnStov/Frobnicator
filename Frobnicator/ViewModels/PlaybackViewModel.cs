using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace Frobnicator.ViewModels
{
    public class PlaybackViewModel
    {
        private readonly AudioOutput.IPlaybackDevice playback;

        public PlaybackViewModel(AudioOutput.IPlaybackDevice playback)
        {
            this.playback = playback;
        }

        public ICommand StartCommand
        {
            get { return new RelayCommand(_ => playback.Start(), _ => !playback.IsPlaying()); }
        }

        public ICommand StopCommand
        {
            get { return new RelayCommand(_ => playback.Stop(), _ => playback.IsPlaying()); }
        }
    }
}