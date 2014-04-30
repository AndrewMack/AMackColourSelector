using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMackColourSelectorModels;
using AMackColourSelectorConsole.ViewModels;

namespace AMackColourSelectorConsole
{
    class Program
    {
        static void Main(string[] args) {
            HSBVM vm = new HSBVM(new HSB(0f, 1f, 0.5f));

            vm.Hue = 120;
            Console.WriteLine(vm.Hue);

            Console.ReadKey();
        }

        static void DisplayColourHSBValues(Colour c) {
            Console.WriteLine(String.Format("Hue: {0}  Saturation: {1}  Brightness: {2}", c.HSB.Hue, c.HSB.Saturation, c.HSB.Brightness));
        }
    }
}
