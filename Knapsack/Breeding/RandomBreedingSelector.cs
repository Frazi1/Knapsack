using System;
using System.Collections;

namespace Knapsack
{
    public class RandomBreedingSelector
    {
        private readonly Random _rnd = new Random();

        public (Species first, Species second) Select(Species[] species)
        {
            int parent1 = _rnd.Next(0, species.Length);
            int parent2 = _rnd.Next(0, species.Length);
            if (parent1 == parent2)
            {
                parent2 = parent2 + 1 > species.Length - 1
                    ? parent2 - 1
                    : parent2 + 1;
            }

            return (species[parent1], species[parent2]);
        }
    }
}