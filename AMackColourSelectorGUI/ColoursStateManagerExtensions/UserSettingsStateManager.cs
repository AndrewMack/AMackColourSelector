using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMackColourSelectorModels;
using AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement;

namespace AMackColourSelectorGUI.ColoursStateManagerExtensions
{
    public class UserSettingsStateManager : StateManager
    {
        private const string DEFEAULT_SETTINGS_NAME = "Colours";
        private string _settingsName;

        public UserSettingsStateManager() {
            _settingsName = DEFEAULT_SETTINGS_NAME;
        }

        public override ICollection<Colour> Load() {
            string coloursString = GetSettingsString();
            string[] colourStrings = coloursString.Split(';');
            AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser stringParser = new AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser();
            List<Colour> colours = new List<Colour>();

            foreach (string colStr in colourStrings) {
                if (!String.IsNullOrWhiteSpace(colStr)) {
                    Colour c;
                    if (stringParser.TryParse(colStr, out c)) {
                        colours.Add(c);
                    }
                }
            }

            return colours;
        }

        public override void Save(ColourCollection colours) {
            try {
                Properties.Settings.Default[_settingsName] = colours.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception) {
                return;
            }
        }

        #region privateMethods
        private string GetSettingsString() {
            string settingsString;

            try {
                settingsString = (string)Properties.Settings.Default[_settingsName];
            }
            catch (System.Configuration.SettingsPropertyNotFoundException) {
                CreateUserSetting();
                settingsString = "";
            }

            return settingsString;
        }

        private void CreateUserSetting() {
            System.Configuration.SettingsProperty property = new System.Configuration.SettingsProperty(_settingsName);
            property.DefaultValue = "";
            property.IsReadOnly = false;
            property.PropertyType = typeof(string);
            property.Attributes.Add(typeof(System.Configuration.UserScopedSettingAttribute), new System.Configuration.UserScopedSettingAttribute());
            property.Provider = Properties.Settings.Default.Providers[""];

            Properties.Settings.Default.Properties.Add(property);
            Properties.Settings.Default.Reload();
        }
        #endregion privateMethods
    }
}
