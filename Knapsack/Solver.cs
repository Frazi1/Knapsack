using System;
using System.Linq;

namespace Knapsack
{
    class Solver
    {
        private readonly Breeder _breeder;
        private readonly IMutator _mutator;
        private readonly SurvivalSelection _survivalSelection;

        private readonly int _maxStepsWithoutChanges;
        private int _stepsWithoutChanges = 0;


        public Species Best { get; private set; }
        public int Steps { get; private set; } = 0;


        public Solver(SurvivalSelection survivalSelection, Breeder breeder, IMutator mutator, int maxStepsWithoutChanges)
        {
            _breeder = breeder;
            _mutator = mutator;
            _maxStepsWithoutChanges = maxStepsWithoutChanges;
            _survivalSelection = survivalSelection;
        }

        private bool UpdateBest(Species[] species)
        {
            Best ??= species[0];

            bool wasUpdated = false;

            foreach (Species s in species)
            {
                if (s.Fittness > Best.Fittness)
                {
                    Best = s;
                    wasUpdated = true;
                }
            }


            if (wasUpdated)
            {
                _stepsWithoutChanges = 0;
                Console.WriteLine($"Current best: {Program.PrintSpecies(Best)} - Step: {Steps}");
            }
            else
            {
                _stepsWithoutChanges++;
            }

            return wasUpdated;
        }

        public Species[] Step(Species[] species)
        {
            Steps++;

            UpdateBest(species);

            var survived = _survivalSelection.Select(species).ToArray();
            var newSpecies = _breeder.Breed(survived).ToArray();
            _mutator.Mutate(newSpecies);
            return newSpecies;
        }

        public Species FindBest(Species[] initial)
        {
            while (_stepsWithoutChanges < _maxStepsWithoutChanges)
            {
                initial = Step(initial);
            }

            return Best;
        }
    }
}