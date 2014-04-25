using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AMackColourSelectorModels
{
    public class Colour : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region privateFields
        private RGB rgb;
        private HSB hsb;
        #endregion privateFields

        #region properties
        public RGB RGB {
            get { return rgb; }
        }
        public HSB HSB {
            get { return hsb; }
        }
        #endregion properties

        #region constructors
        public Colour(RGB colourRGB) {
            rgb = new RGB(colourRGB.R, colourRGB.B, colourRGB.B);
            SetHSBFromRGB();
        }

        public Colour(HSB colourHSB) {
            hsb = new HSB(colourHSB.Hue, colourHSB.Saturation, colourHSB.Brightness);
            SetRGBFromHSB();
        }
        #endregion constructors

        #region publicMethods
        public void SetColour(RGB colourRGB) {
            rgb.SetRGB(colourRGB.R, colourRGB.G, colourRGB.B);
            SetHSBFromRGB();
        }

        public void SetColour(HSB colourHSB) {
            hsb.SetHSB(colourHSB.Hue, colourHSB.Saturation, colourHSB.Brightness);
        }

        public System.Drawing.Color GetSystemDrawingColour() {
            return System.Drawing.Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }

        public void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion publicMethods

        #region privateMethods
        private void SetHSBFromRGB() {
            hsb = rgb.GetHSB();
        }

        private void SetRGBFromHSB() {
            rgb = hsb.GetRGB();
        }
        #endregion privateMethods
    }
}
