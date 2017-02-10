using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Frobnicator.Annotations;

namespace Frobnicator.ViewModels
{
   public class PlaybackViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      public ICommand StartCommand { get; } = new RelayCommand(_ => MessageBox.Show("Start"));
      public ICommand StopCommand { get; } = new RelayCommand(_ => MessageBox.Show("Stop"));

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}