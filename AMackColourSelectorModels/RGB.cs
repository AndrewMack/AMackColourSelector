using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMackColourSelectorModels
{
    public class RGB
    {
        private byte _r;
        private byte _g;
        private byte _b;

        public byte R {
            get { return _r; }
        }
        public byte G {
            get { return _g; }
        }
        public byte B {
            get { return _b; }
        }

        public RGB(byte r, byte g, byte b) {
            _r = r;
            _g = g;
            _b = b;
        }

        public RGB(int r, int g, int b) {
            ChangeValuesWhereNeeded(ref r, ref g, ref b);
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
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
