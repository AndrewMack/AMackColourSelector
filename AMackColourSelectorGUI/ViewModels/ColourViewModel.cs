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
                _hue = value;
                RaiseHSBPropertyAndColourChange("Hue");
            }
        }

        public int Saturation {
            get { return _saturation; }
            set {
                _saturation = value;
                RaiseHSBPropertyAndColourChange("Saturation");
            }
        }

        public int Brightness {
            get { return _brightness; }
            set {
                _brightness = value;
                RaiseHSBPropertyAndColourChange("Brightness");
            }
        }

        public System.Windows.Media.Color MediaColour {
            get { return _mediaColor; }
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
            float saturation = (float)(1f - (point.Y / size.Height));
            float brightness = (float)(_brightness / 100f);

            _colour.SetColour(new HSB(hue, saturation, brightness));

            ResetHSBValues();
            ResetRGBValues();
            ResetMediaColour();

            RaiseCompleteColourChange();
        }

        private void RaiseRGBPropertyAndColourChange(string propertyName) {
            RaisePropertyChanged(propertyName);
            _colour.SetColour(new RGB(_r, _g, _b));
            ResetHSBValues();
            ResetMediaColour();
            RaiseHSBPropertiesChanged();
            RaiseMediaColourChanged();
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

        private void RaiseCompleteColourChange() {
            RaiseRGBPropertiesChanged();
            RaiseHSBPropertiesChanged();
            RaiseMediaColourChanged();
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

        private void SetValuesFromColour(Colour colour) {
            _colour.SetColour(colour.RGB);

            _r = _colour.RGB.R;
            _g = _colour.RGB.G;
            _b = _colour.RGB.B;

            _hue = (int)colour.HSB.Hue;
            _saturation = (int)(colour.HSB.Saturation * 100f);
            _brightness = (int)(colour.HSB.Brightness * 100f);

            _mediaColor = System.Windows.Media.Color.FromRgb(_r, _g, _b);
        }

        public void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
