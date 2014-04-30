using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMackColourSelectorModels;
using System.ComponentModel;

namespace AMackColourSelectorModels.ViewModels
{
    public class RGBVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private RGB rgb;
        private int _r;
        private int _g;
        private int _b;

        public int R {
            get { return _r; }
            set {
                int r = value;
                if (r < 0) {
                    r = 0;
                }
                else if (r > 255) {
                    r = 255;
                }
                _r = r;

                SetRGB();
            }
        }

        public int G {
            get { return _g; }
            set {
                int g = value;
                if (g < 0) {
                    g = 0;
                }
                else if (g > 255) {
                    g = 255;
                }
                _g = g;

                SetRGB();
            }
        }

        public int B {
            get { return _b; }
            set {
                int b = value;
                if (b < 0) {
                    b = 0;
                }
                else if (b > 255) {
                    b = 255;
                }
                _b = b;

                SetRGB();
            }
        }

        public RGBVM(RGB rgb) {
            this.rgb = rgb;
        }

        public void RaiseRGBChanged() {
            RaisePropertyChanged("R");
            RaisePropertyChanged("G");
            RaisePropertyChanged("B");
        }

        public void SetRGB() {
            rgb.SetRGB(_r, _g, _b);
        }

        public void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
