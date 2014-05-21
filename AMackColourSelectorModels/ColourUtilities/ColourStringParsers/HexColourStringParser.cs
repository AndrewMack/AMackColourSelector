using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.ColourStringParsers
{
    public class HexColourStringParser : IColourStringParser
    {
        public Colour Parse(string stringToParse) {
            string s = stringToParse;
            int charCount = s.Length;
            Colour colourFromString;
            bool stringCouldBeHex = false;

            if (charCount == 3 || charCount == 4) {
                // #FAC or FAC
                // which means we double up each
                // #FAC == #FFAACC
                // FAC == FFAACC

                s = s.Substring(s.Length - 3); // trims the '#' if it has it

                // flesh out the hex value
                char[] hexDigits = { s[0], s[0], s[1], s[1], s[2], s[2] };
                s = String.Concat(hexDigits);

                stringCouldBeHex = true;
            }
            else if (charCount >= 6) {
                s = s.Substring(s.Length - 6);

                stringCouldBeHex = true;
            }

            if (stringCouldBeHex) {
                int r = Convert.ToInt32(s.Substring(0, 2), 16);
                int g = Convert.ToInt32(s.Substring(2, 2), 16);
                int b = Convert.ToInt32(s.Substring(4, 2), 16);

                colourFromString = new Colour(new RGB(r, g, b));

                return colourFromString;
            }

            return null; // raise exception instead
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
