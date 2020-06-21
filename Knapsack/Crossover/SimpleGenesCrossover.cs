using System;
using System.Collections;

namespace Knapsack
{
    public class SimpleGenesCrossover : IGenesCrossover
    {
        private readonly IFitnessFun _fitnessFun;
        private readonly Random _rnd = new Random();

        private readonly double _betterParentGeneSelectionProbability;
        
        public SimpleGenesCrossover(IFitnessFun fitnessFun, double betterParentGeneSelectionProbability)
        {
            _fitnessFun = fitnessFun;
            _betterParentGeneSelectionProbability = betterParentGeneSelectionProbability;
        }

        public Species Produce(Species parent1, Species parent2)
        {
            BitArray bitArray = new BitArray(parent1.Values.Length);

            var better = parent1.Fittness >= parent2.Fittness ? parent1 : parent2;
            var worse = better == parent1 ? parent2 : parent1;
            
            for (var i = 0; i < bitArray.Count; i++)
            {

                bitArray[i] = _rnd.NextDouble() >= 1 - _betterParentGeneSelectionProbability
                    ? better.Values[i]
                    : worse.Values[i];
            }

            var result = new Species(bitArray, _fitnessFun.Calculate(bitArray));
            return result;
        }
    }
}