using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMackColourSelectorModels
{
    public class RGB
    {
        #region privateFields
        private byte _r;
        private byte _g;
        private byte _b;
        #endregion privateFields

        #region properties
        public byte R {
            get { return _r; }
        }
        public byte G {
            get { return _g; }
        }
        public byte B {
            get { return _b; }
        }
        #endregion properties

        #region constructors
        public RGB(byte r, byte g, byte b) {
            SetRGB(r, g, b);
        }

        public RGB(int r, int g, int b) {
            SetRGB(r, g, b);
        }
        #endregion constructors

        public void SetRGB(byte r, byte g, byte b) {
            _r = r;
            _g = g;
            _b = b;
        }

        public void SetRGB(int r, int g, int b) {
            ChangeValuesWhereNeeded(ref r, ref g, ref b);
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
        }

        public HSB GetHSB() {
            System.Drawing.Color c = System.Drawing.Color.FromArgb(_r, _g, _b);
            return new HSB(c.GetHue(), c.GetSaturation(), c.GetBrightness());
        }

        private void ChangeValuesWhereNeeded(ref int r, ref int g, ref int b) {
            ChangeSingleValueIfNeeded(ref r);
            ChangeSingleValueIfNeeded(ref g);
            ChangeSingleValueIfNeeded(ref b);
        }

        private void ChangeSingleValueIfNeeded(ref int val) {
            if (val < 0) {
                val = 0;
            }
            else if (val > 255) {
                val = 255;
            }
        }
    }
}
