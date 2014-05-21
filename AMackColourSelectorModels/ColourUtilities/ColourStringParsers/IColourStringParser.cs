using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.ColourStringParsers
{
    public interface IColourStringParser
    {
        Colour Parse(string stringToParse);
        bool TryParse(string stringToParse, out Colour parsedColour);
    }
}
