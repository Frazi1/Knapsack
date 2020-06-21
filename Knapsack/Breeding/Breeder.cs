using System.Collections.Generic;
using System.Linq;

namespace Knapsack
{
    public class Breeder
    {
        private readonly RandomBreedingSelector _breedingSelector;
        private readonly IGenesCrossover _crossover;

        public int PopulationCount { get; }


        public Breeder(int populationCount, RandomBreedingSelector breedingSelector, IGenesCrossover crossover)
        {
            _breedingSelector = breedingSelector;
            _crossover = crossover;
            PopulationCount = populationCount;
        }

        public IEnumerable<Species> Breed(Species[] species)
        {
            return Enumerable
                .Repeat(0, PopulationCount)
                .Select(_ => _breedingSelector.Select(species))
                .Select(pair => _crossover.Produce(pair.first, pair.second));
        }
    }
}