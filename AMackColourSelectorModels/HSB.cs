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

        public RGB GetRGB() {
            RGB rgb = CalculateToRGB();
            return rgb;
        }

        private void SetHue(float hue) {
            if (hue < 0f || hue >= 360f) {
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

        private RGB CalculateToRGB() {
            float hue, saturation, brightness;
            hue = _hue;
            saturation = _saturation;
            brightness = _brightness;

            if (saturation == 0) {
                int r = (int)(brightness * 255);
                return new RGB(r, r, r);
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

            if (0.5 < brightness) {
                fMax = brightness - (brightness * saturation) + saturation;
                fMin = brightness + (brightness * saturation) - saturation;
            }
            else {
                fMax = brightness + (brightness * saturation);
                fMin = brightness - (brightness * saturation);
            }

            iSextant = (int)Math.Floor(hue / 60f);
            if (300f <= hue) {
                hue -= 360f;
            }

            hue /= 60f;
            hue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2) {
                fMid = (hue * (fMax - fMin)) + fMin;
            }
            else {
                fMid = fMin - (hue * (fMax - fMin));
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            RGB rgb;

            switch (iSextant) {
                case 1:
                    rgb = new RGB(iMid, iMax, iMin); break;
                case 2:
                    rgb = new RGB(iMin, iMax, iMid); break;
                case 3:
                    rgb = new RGB(iMin, iMid, iMax); break;
                case 4:
                    rgb = new RGB(iMid, iMin, iMax); break;
                case 5:
                    rgb = new RGB(iMax, iMin, iMid); break;
                default:
                    rgb = new RGB(iMax, iMid, iMin); break;
            }

            return rgb;
        }
    }
}
