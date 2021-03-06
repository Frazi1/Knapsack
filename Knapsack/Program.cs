﻿using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Knapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            string datasetFolder = @"G:\_Projects\Knapsack\Knapsack\Knapsack\datasets";
            Directory.SetCurrentDirectory(datasetFolder);


            while (true)
            {
                Run();
                Console.WriteLine();
            }
        }

        private static void Run()
        {
            const int initialSpeciesNumber = 4;
            const int survivalSelectionThreshold = 3;
            const double mutationProbability = 0.3;
            const double betterParentGeneSelectionProbability = 0.8;


            Console.WriteLine("Dataset name:");
            string datasetName = Console.ReadLine();

            Console.WriteLine("Accuracy");
            int maxStepsWithoutChanges = int.Parse(Console.ReadLine());

            var settings = Settings.ParseDataSet(datasetName);

            var fitnessFun = new SimpleFitnessFun(settings.MaxVolume, settings.Profits, settings.Weights);
            var survivalSelection = new SurvivalSelection(survivalSelectionThreshold);
            IGenesCrossover crossover = new SimpleGenesCrossover(fitnessFun, betterParentGeneSelectionProbability);
            Breeder breeder = new Breeder(initialSpeciesNumber, new RandomBreedingSelector(), crossover);
            var mutator = new RandomMutator(mutationProbability);
            var solver = new Solver(survivalSelection, breeder, mutator, maxStepsWithoutChanges);


            var initialSpecies = GenerateInitialSpecies(initialSpeciesNumber, settings.Profits.Length, fitnessFun);


            Console.WriteLine("Начальные особи");
            PrintSpecies(initialSpecies);


            // while (true)
            // {
            //     initialSpecies = solver.Step(initialSpecies);
            //     PrintSpecies(initialSpecies);
            //     Console.WriteLine();
            //
            //     var key = Console.ReadKey();
            //     if (key.Key == ConsoleKey.X) break;
            // }

            Species best = solver.FindBest(initialSpecies);

            Console.WriteLine();
            Console.WriteLine($"Calcula best:{PrintSpecies(best)}. Steps: {solver.Steps}");
            if (settings.DatasetBest != null)
            {
                Console.WriteLine($"Dataset best:{PrintSpecies(settings.DatasetBest)}");
            }
        }


        public static string PrintSpecies(Species species)
        {
            string genes = string.Join(' ', species.Values.Cast<bool>().Select(b => b ? '1' : '0'));
            return $"{genes} - {species.Fittness}";
        }

        public static void PrintSpecies(Species[] species)
        {
            foreach (Species bitArray in species)
            {
                Console.WriteLine(PrintSpecies(bitArray));
            }

            Console.WriteLine();
        }

        private static Species[] GenerateInitialSpecies(int speciesNumber, int itemCount, IFitnessFun fitnessFun)
        {
            var rnd = new Random();

            return Enumerable.Range(1, speciesNumber)
                .Select(_ =>
                {
                    var vector = new BitArray(itemCount);
                    for (var i = 0; i < vector.Count; i++)
                    {
                        bool value = rnd.Next(0, 2) == 1;
                        vector[i] = value;
                    }

                    return vector;
                })
                .Select(v => new Species(v, fitnessFun.Calculate(v)))
                .ToArray();
        }
    }
}