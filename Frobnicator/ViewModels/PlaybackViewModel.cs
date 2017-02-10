using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace Frobnicator.ViewModels
{
   public class PlaybackViewModel
   {
      private readonly Playback.PlaybackDevice playback = new Playback.PlaybackDevice(0, 44100.0);

      public ICommand StartCommand
      {
         get { return new RelayCommand(_ => playback.start(), _ => !playback.isPlaying()); }
      }

      public ICommand StopCommand
      {
         get { return new RelayCommand(_ => playback.stop(), _ => playback.isPlaying()); }
      }
   }
}