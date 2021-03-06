﻿using System;
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
        public System.Windows.Media.Color MediaColor {
            get { return System.Windows.Media.Color.FromRgb(rgb.R, rgb.G, rgb.B); }
        }
        #endregion properties

        #region constructors
        public Colour(RGB colourRGB) {
            rgb = new RGB(colourRGB.R, colourRGB.G, colourRGB.B);
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
            SetRGBFromHSB();
        }

        public bool IsSameColour(Colour c) {
            return rgb.Equals(c.rgb);
        }

        public bool IsSameColour(RGB rgb) {
            return this.rgb.Equals(rgb);
        }

        public bool IsSameColour(HSB hsb) {
            return this.hsb.Equals(hsb);
        }

        public override string ToString() {
            return String.Format("{0},{1},{2}", rgb.R, rgb.G, rgb.B);
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
        #region publicStaticMethods
        public static bool TryParse(string colourString, out Colour c) {
            c = null;

            return true;
        }
        #endregion publicStaticMethods

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
