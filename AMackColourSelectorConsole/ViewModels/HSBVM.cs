using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AMackColourSelectorModels;

namespace AMackColourSelectorConsole.ViewModels
{
    public class HSBVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private HSB hsb;
        private int _hue;
        private int _saturation;
        private int _brightness;
        private float _h;
        private float _s;
        private float _b;

        public int Hue {
            get { return _hue; }
            set {
                int hue = value;
                if (hue < 0) {
                    hue = 0;
                }
                else if (hue > 360) {
                    hue = 360;
                }
                _hue = hue;
                _h = (float)_hue;

                SetHSB();

                RaisePropertyChanged("Hue");
            }
        }

        public int Saturation {
            get { return _saturation; }
            set {
                int saturation = value;
                if (saturation < 0) {
                    saturation = 0;
                }
                else if (saturation > 100) {
                    saturation = 100;
                }
                _saturation = saturation;
                _s = (float)((float)saturation / 100f);

                SetHSB();

                RaisePropertyChanged("Saturation");
            }
        }

        public int Brightness {
            get { return _brightness; }
            set {
                int brightness = value;
                if (brightness < 0) {
                    brightness = 0;
                }
                else if (brightness > 100) {
                    brightness = 100;
                }

                SetHSB();

                _brightness = brightness;
                _b = (float)((float)brightness / 100f);
            }
        }

        public HSBVM(HSB hsb) {
            this.hsb = hsb;
        }

        public void RaiseHSBChange() {
            RaisePropertyChanged("Hue");
            RaisePropertyChanged("Saturation");
            RaisePropertyChanged("Brightness");
        }

        public void SetHSB() {
            hsb.SetHSB(_h, _s, _b);
        }

        public void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
