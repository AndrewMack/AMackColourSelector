using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement
{
    public class RegistryStateManager : StateManager
    {
        const char COLOUR_DELIMITER = ';';
        private RegistryKey _coloursRegistryKey;
        private string _registryKeyName;

        #region constructors
        public RegistryStateManager(RegistryKey regKey, string name) {
            _coloursRegistryKey = regKey;
            _registryKeyName = name;
        }

        public RegistryStateManager() { }
        #endregion constructors

        public void SetRegistryKey(RegistryKey regKey, string name) {
            _coloursRegistryKey = regKey;
            _registryKeyName = name;
        }

        override public ICollection<Colour> Load() {
            List<Colour> colours;

            try {

                colours = GetListOfColoursFromRegistry();
            }
            catch (Exception) {
                colours = new List<Colour>();
            }

            return colours;
        }

        override public void Save(ColourCollection colours) {
            string coloursString = colours.ToString();

            _coloursRegistryKey.SetValue(_registryKeyName, coloursString);
        }

        #region HelperMethodsForGET
        private string GetColoursRegistryValue() {
            return (string)_coloursRegistryKey.GetValue(_registryKeyName);
        }

        private List<Colour> GetListOfColoursFromRegistry() {
            ColourStringParsers.RGBColourStringParser stringParser = new ColourStringParsers.RGBColourStringParser();
            string regValue = GetColoursRegistryValue();
            List<Colour> colours = new List<Colour>();
            string[] coloursStrings = regValue.Split(COLOUR_DELIMITER);

            foreach (string colourString in coloursStrings) {
                if (!String.IsNullOrEmpty(colourString)) {
                    Colour c;
                    if (stringParser.TryParse(colourString, out c)) {
                        colours.Add(c);
                    }
                }
            }

            return colours;
        }
        #endregion HelperMethodsForGET
    }
}
