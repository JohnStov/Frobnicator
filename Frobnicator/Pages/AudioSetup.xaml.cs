using System.Windows.Controls;
using Frobnicator.ViewModels;

namespace Frobnicator.Pages
{
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AudioSetup : UserControl
    {
        public AudioSetup()
        {
            InitializeComponent();

            DataContext = new AudioSetupViewModel(App.AudioOutput, App.AudioOutput);
        }
    }
}