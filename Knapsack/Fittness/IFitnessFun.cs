using System.Collections;

namespace Knapsack
{
    public interface IFitnessFun
    {
        int Calculate(BitArray species);
    }
}