using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement
{
    public class VoidStateManager : StateManager
    {
        public override ICollection<Colour> Load() {
            return new Colour[0];
        }

        public override void Save(ColourCollection colourCollection) {
            return;
        }
    }
}
