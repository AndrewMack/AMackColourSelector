using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.ColourStringParsers
{
    public class RGBColourStringParser : IColourStringParser
    {
        public Colour Parse(string stringToParse) {
            string s = stringToParse.Replace(" ", String.Empty);
            string[] rgbStringVals = s.Split(',');

            if (rgbStringVals.Count() != 3) {
                throw new FormatException("RGB string should have only 3 values.");
            }
            else {
                int r, g, b;
                r = Int32.Parse(rgbStringVals[0]);
                g = Int32.Parse(rgbStringVals[1]);
                b = Int32.Parse(rgbStringVals[2]);

                return new Colour(new RGB(r, g, b));
            }
        }

        public bool TryParse(string stringToParse, out Colour colour) {
            try {
                colour = Parse(stringToParse);
                return true;
            }
            catch (Exception) {
                colour = null;
                return false;
            }
        }
    }
}
