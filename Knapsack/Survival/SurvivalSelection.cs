using System.Collections.Generic;
using System.Linq;

namespace Knapsack
{
    class SurvivalSelection
    {
        public int SelectionThreshold { get; }

        public SurvivalSelection(int selectionThreshold)
        {
            SelectionThreshold = selectionThreshold;
        }

        public IEnumerable<Species> Select(Species[] species)
        {
            return species.OrderBy(s => s.Fittness).Take(SelectionThreshold);
        }
    }
}