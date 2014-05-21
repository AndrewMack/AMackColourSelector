using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMackColourSelectorModels.ColourUtilities.CustomColoursUtility.StateManagement
{
    public interface IManageable
    {
        ICollection<Colour> Load();
        void Save(ColourCollection colourCollection);
    }
}
