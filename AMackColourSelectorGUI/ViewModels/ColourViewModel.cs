using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMackColourSelectorModels;
using System.ComponentModel;

namespace AMackColourSelectorGUI.ViewModels
{
    public class ColourViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region privateFields
        private Colour _initialColour;
        private Colour _colour;
        
        private byte _r;
        private byte _b;
        private byte _g;

        private int _hue;
        private int _saturation;
        private int _brightness;

        private System.Windows.Media.Color _mediaColor;
        private System.Windows.Media.Color _midBrightnessColor;
        #endregion privateFields

        #region publicProperties
        public byte R {
            get { return _r; }
            set {
                _r = value;
                RaiseRGBPropertyAndColourChange("R");
            }
        }

        public byte G {
            get { return _g; }
            set {
                _g = value;
                RaiseRGBPropertyAndColourChange("G");
            }
        }

        public byte B {
            get { return _b; }
            set {
                _b = value;
                RaiseRGBPropertyAndColourChange("B");
            }
        }

        public int Hue {
            get { return _hue; }
            set {
                int hue = value;
                if (hue >= 360) { hue = 359; }
                else if (hue < 0) { hue = 0; }
                _hue = hue;
                RaiseHSBPropertyAndColourChange("Hue");
            }
        }

        public int Saturation {
            get { return _saturation; }
            set {
                int saturation = value;
                if (saturation > 100) { saturation = 100; }
                else if (saturation < 0) { saturation = 0; }
                _saturation = saturation;
                RaiseHSBPropertyAndColourChange("Saturation");
            }
        }

        public int Brightness {
            get { return _brightness; }
            set {
                int brightness = value;
                if (brightness > 100) { brightness = 100; }
                else if (brightness < 0) { brightness = 0; }
                _brightness = brightness;
                RaiseHSBPropertyAndColourChange("Brightness");
            }
        }

        public System.Windows.Media.Color MediaColour {
            get { return _mediaColor; }
        }

        public System.Windows.Media.Color MidBrightnessMediaColor {
            get { return _midBrightnessColor; }
        }

        public Colour CurrentColour {
            get { return _colour; }
        }

        public Colour InitialColour {
            get { return _initialColour; }
        }
        #endregion publicProperties

        #region constructors
        public ColourViewModel() {
            Colour colour = new Colour(new RGB(255, 0, 0));
            _initialColour = colour;
            _colour = new Colour(colour.RGB);
            SetValuesFromColour(colour);
        }

        public ColourViewModel(Colour colour) {
            _initialColour = colour;
            _colour = new Colour(colour.RGB);
            SetValuesFromColour(colour);
        }
        #endregion constructors

        public void SetColourFromPointInSize(System.Windows.Point point, System.Windows.Size size) {
            float hue = (float)((point.X / size.Width) * 360);
            if (hue >= 360f) {
                hue = 359f;
            }

            float saturation = (float)(1f - (point.Y / size.Height));
            float brightness = (float)(_brightness / 100f);

            _colour.SetColour(new HSB(hue, saturation, brightness));

            ResetHSBValues();
            ResetRGBValues();
            ResetMediaColour();
            ResetMidBrightColour();

            RaiseCompleteColourChange();
        }

        private void RaiseRGBPropertyAndColourChange(string propertyName) {
            RaisePropertyChanged(propertyName);
            _colour.SetColour(new RGB(_r, _g, _b));
            ResetHSBValues();
            ResetMediaColour();
            ResetMidBrightColour();

            RaiseHSBPropertiesChanged();
            RaiseMediaColourChanged();
            RaiseMidBrightColourChanged();
        }

        private void RaiseHSBPropertyAndColourChange(string propertyName) {
            RaisePropertyChanged(propertyName);
            float hue = (float)_hue;
            float sat = (float)_saturation / 100f;
            float bri = (float)_brightness / 100f;

            _colour.SetColour(new HSB(hue, sat, bri));
            ResetRGBValues();
            ResetMediaColour();
            
            RaiseRGBPropertiesChanged();
            RaiseMediaColourChanged();

            if (propertyName.ToLower() != "brightness") {
                ResetMidBrightColour();
                RaiseMidBrightColourChanged();
            }
        }

        private void RaiseRGBPropertiesChanged() {
            RaisePropertyChanged("R");
            RaisePropertyChanged("G");
            RaisePropertyChanged("B");
        }

        private void RaiseHSBPropertiesChanged() {
            RaisePropertyChanged("Hue");
            RaisePropertyChanged("Saturation");
            RaisePropertyChanged("Brightness");
        }

        private void RaiseMediaColourChanged() {
            RaisePropertyChanged("MediaColour");
        }

        private void RaiseMidBrightColourChanged() {
            RaisePropertyChanged("MidBrightnessMediaColor");
        }

        private void RaiseCompleteColourChange() {
            RaiseRGBPropertiesChanged();
            RaiseHSBPropertiesChanged();
            RaiseMediaColourChanged();
            RaiseMidBrightColourChanged();
        }

        private void ResetRGBValues() {
            _r = _colour.RGB.R;
            _g = _colour.RGB.G;
            _b = _colour.RGB.B;
        }

        private void ResetHSBValues() {
            _hue = (int)_colour.HSB.Hue;
            _saturation = (int)(_colour.HSB.Saturation * 100f);
            _brightness = (int)(_colour.HSB.Brightness * 100f);
        }

        private void ResetMediaColour() {
            _mediaColor = System.Windows.Media.Color.FromRgb(_r, _g, _b);
        }

        private void ResetMidBrightColour() {
            _midBrightnessColor = GetMidBrightColor();
        }

        private void SetValuesFromColour(Colour colour) {
            _colour.SetColour(colour.RGB);

            _r = _colour.RGB.R;
            _g = _colour.RGB.G;
            _b = _colour.RGB.B;

            _hue = (int)colour.HSB.Hue;
            _saturation = (int)(colour.HSB.Saturation * 100f);
            _brightness = (int)(colour.HSB.Brightness * 100f);

            _mediaColor = System.Windows.Media.Color.FromRgb(_r, _g, _b);
            _midBrightnessColor = GetMidBrightColor();
        }

        private System.Windows.Media.Color GetMidBrightColor() {
            HSB hsb = new HSB(_colour.HSB.Hue, _colour.HSB.Saturation, 0.5f);
            RGB rgb = hsb.GetRGB();
            return System.Windows.Media.Color.FromRgb(rgb.R, rgb.G, rgb.B);
        }

        public void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
