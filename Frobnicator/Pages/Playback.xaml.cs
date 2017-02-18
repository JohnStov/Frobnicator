using System.Windows.Controls;
using Frobnicator.ViewModels;

namespace Frobnicator.Pages
{
    /// <summary>
    ///     Interaction logic for Playback.xaml
    /// </summary>
    public partial class Playback : UserControl
    {
        public Playback()
        {
            InitializeComponent();

            DataContext = new PlaybackViewModel(App.AudioOutput, App.MidiInput);
        }
    }
}