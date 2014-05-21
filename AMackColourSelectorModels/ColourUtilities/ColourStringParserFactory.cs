using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMackColourSelectorModels.ColourUtilities;

namespace AMackColourSelectorModels.ColourUtilities
{
    public class ColourStringParserFactory
    {
        // hex -- ("FF00CC", "F0C", "#FF00CC", "#F0C")
        // rgb -- ("255, 0, 204", "(255, 0, 204)")
        // WinDev -- 557419

        public enum Parsers
        {
            Hex = 1,
            RGB = 2,
            WinDev = 3
        }
    }
}
