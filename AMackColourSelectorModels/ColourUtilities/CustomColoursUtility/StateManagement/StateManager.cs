using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement
{
    public abstract class StateManager : IManageable
    {
        abstract public ICollection<Colour> Load();
        abstract public void Save(ColourCollection colourCollection);
    }
}
