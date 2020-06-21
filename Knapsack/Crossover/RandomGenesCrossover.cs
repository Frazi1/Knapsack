using System;
using System.Collections;

namespace Knapsack
{
    public class RandomGenesCrossover : IGenesCrossover
    {
        private readonly IFitnessFun _fitnessFun;
        private Random _rnd = new Random();

        public RandomGenesCrossover(IFitnessFun fitnessFun)
        {
            _fitnessFun = fitnessFun;
        }
        
        public Species Produce(Species parent1, Species parent2)
        {
            BitArray bitArray = new BitArray(parent1.Values.Length);

            for (var i = 0; i < bitArray.Count; i++)
            {
                bitArray[i] = _rnd.Next(0, 2) == 1 ? parent1.Values[i] : parent2.Values[i];
            }

            var result = new Species(bitArray, _fitnessFun.Calculate(bitArray));
            return result;
        }
    }
}