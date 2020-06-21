using System.Collections;

namespace Knapsack
{
    public class Species
    {
        public BitArray Values { get;}
        public int Fittness { get; }

        public Species(BitArray values, int fittness)
        {
            Values = values;
            Fittness = fittness;
        }
    }
}