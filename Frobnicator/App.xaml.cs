using System.Windows;

namespace Frobnicator
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AudioOutput.AudioOutput AudioOutput { get; } = new AudioOutput.AudioOutput(44100.0);
    }
}