using System.Windows.Controls;
using Frobnicator.ViewModels;

namespace Frobnicator.Pages
{
    public partial class MidiSetup : UserControl
    {
        public MidiSetup()
        {
            InitializeComponent();

            DataContext = new MidiSetupViewModel(App.MidiInput, App.MidiInput);
        }
    }
}