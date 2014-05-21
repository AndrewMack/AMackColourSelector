using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement;

namespace AMackColourSelectorModels
{
    public class ColourCollection : ObservableCollection<Colour>
    {
        private StateManager stateManager;

        #region constructors
        public ColourCollection() {
            stateManager = new VoidStateManager();
        }

        public ColourCollection(StateManager sm) {
            stateManager = sm;
            ICollection<Colour> colours = stateManager.Load();
            FillColourCollection(colours);
        }

        public ColourCollection(ICollection<Colour> colours) {
            FillColourCollection(colours);
        }
        #endregion constructors

        #region publicMethods
        public void Save() {
            if (stateManager != null) {
                stateManager.Save(this);
            }
        }

        public override string ToString() {
            return String.Join(";", this);
        }

        public ICollection<Colour> GetICollection() {
            return this.ToArray<Colour>();
        }
        #endregion publicMethods

        private void FillColourCollection(ICollection<Colour> colours) {
            this.Clear();
            foreach (Colour c in colours) {
                this.Add(c);
            }
        }
    }
}
