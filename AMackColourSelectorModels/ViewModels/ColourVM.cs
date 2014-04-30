using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AMackColourSelectorModels.ViewModels
{
    public class ColourVM : INotifyPropertyChanged
    {
        private Colour _colour;
        private RGBVM _rgb;
        private HSBVM _hsb;

        public RGBVM RGB {
            get { return _rgb; }
        }
        public HSBVM HSB {
            get { return _hsb; }
        }

        public ColourVM(Colour c) {
            _colour = c;
            _rgb = new RGBVM(c.RGB);
            _hsb = new HSBVM(c.HSB);
        }
    }
}
