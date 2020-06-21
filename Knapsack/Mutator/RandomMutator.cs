using System;

namespace Knapsack
{
    public class RandomMutator : IMutator
    {
        private readonly Random _rnd = new Random();
        
        public double MutationProbability { get; }

        public RandomMutator(double mutationProbability)
        {
            MutationProbability = mutationProbability;
        }
        
        public void Mutate(Species[] species)
        {
            foreach (Species s in species)
            {
                if (!ShouldMutate()) continue;
                int geneIndex = _rnd.Next(0, s.Values.Length);
                s.Values[geneIndex] = !s.Values[geneIndex];
            }
        }

        private bool ShouldMutate() => _rnd.NextDouble() < MutationProbability;
    }
}