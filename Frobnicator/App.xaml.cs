using System.Windows;

namespace Frobnicator
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly AudioOutput.AudioOutput Output = new AudioOutput.AudioOutput(44100.0);
        private static readonly MidiInput.MidiInput Input = new MidiInput.MidiInput();

        public static AudioOutput.AudioOutput AudioOutput { get; } = Output;
        public static MidiInput.MidiInput MidiInput { get; } = Input;
    }
}