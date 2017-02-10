using System.Collections.Generic;

namespace Frobnicator.ViewModels
{
   public class MidiSetupViewModel
   {
      public IEnumerable<string> DeviceNames { get; } = new[] {"Device1", "Device2"};
   }
}
