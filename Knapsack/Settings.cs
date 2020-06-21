using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Knapsack
{
    public class Settings
    {
        public int MaxVolume { get; }
        public int[] Weights { get; }
        public int[] Profits { get; }
        
        public Species DatasetBest { get; }

        public Settings(int maxVolume, int[] weights, int[] profits, Species datasetBest)
        {
            MaxVolume = maxVolume;
            Weights = weights;
            Profits = profits;
            DatasetBest = datasetBest;
        }

        public static Settings ParseDataSet(string datasetName)
        {
            string capacityFile = $"{datasetName}_c.txt";
            int capacity = File.ReadLines(capacityFile).Select(l => l.Trim()).Select(int.Parse).First();

            string weightFile = $"{datasetName}_w.txt";
            var weights = File.ReadLines(weightFile).Select(l => l.Trim()).Select(int.Parse).ToArray();

            string profitsFile = $"{datasetName}_p.txt";
            var profits = File.ReadLines(profitsFile).Select(l => l.Trim()).Select(int.Parse).ToArray();

            string optimalFile = $"{datasetName}_s.txt";
            var dataSetBestArray = File.ReadLines(optimalFile).Select(l => l.Trim()).Select(int.Parse).Select(i => i == 1)
                .ToArray();
            
            var simpleFitnessFun = new SimpleFitnessFun(capacity, profits, weights);
            BitArray bitArray = new BitArray(dataSetBestArray);
            var datasetBest = new Species(bitArray, simpleFitnessFun.Calculate(bitArray));
            
            return new Settings(capacity, weights, profits, datasetBest);
        }

        public static Settings ReadFromKeyBoard()
        {
            int maxVolume = int.Parse(Console.ReadLine());
            Console.WriteLine("Ценность предметов");
            var worth = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();


            Console.WriteLine("Вес предметов");
            var weight = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            if (worth.Length != weight.Length) throw new Exception("Number of items is incorrect");
            
            return new Settings(maxVolume, weight, worth, null);
        }
    }
}