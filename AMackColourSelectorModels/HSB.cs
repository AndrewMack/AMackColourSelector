using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AMackColourSelectorModels
{
    public class HSB
    {
        #region privateFields
        private float _hue;
        private float _saturation;
        private float _brightness;
        #endregion privateFields

        #region properties
        public float Hue {
            get { return _hue; }
        }
        public float Saturation {
            get { return _saturation; }
        }
        public float Brightness {
            get { return _brightness; }
        }
        #endregion properties

        #region constructors
        public HSB() {
            _hue = 0f;
            _saturation = 0f;
            _brightness = 0f;
        }

        public HSB(float hue, float sat, float bri) {
            SetHue(hue);
            SetSaturation(sat);
            SetBrightness(bri);
        }
        #endregion constructors

        public void SetHSB(float hue, float sat, float bri) {
            SetHue(hue);
            SetSaturation(sat);
            SetBrightness(bri);
        }

        private void SetHue(float hue) {
            if (hue < 0f || hue >= 360) {
                hue = 0f;
            }
            _hue = hue;
        }

        private void SetSaturation(float saturation) {
            if (saturation < 0f) {
                saturation = 0f;
            }
            else if (saturation > 1f) {
                saturation = 1f;
            }
            _saturation = saturation;
        }

        private void SetBrightness(float brightness) {
            if (brightness < 0f) {
                brightness = 0f;
            }
            else if (brightness > 1f) {
                brightness = 1f;
            }
            _brightness = brightness;
        }
    }
}
