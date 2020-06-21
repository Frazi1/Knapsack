using System.Collections;

namespace Knapsack
{
    class SimpleFitnessFun : IFitnessFun
    {
        public int MaxVolume { get; }
        public int[] Worths { get; }
        public int[] Weights { get; }

        public SimpleFitnessFun(int maxVolume, int[] worths, int[] weights)
        {
            MaxVolume = maxVolume;
            Worths = worths;
            Weights = weights;
        }
        
        public int Calculate(BitArray species)
        {
            int sumWeight = 0;
            int sumWorth = 0;

            for (var i = 0; i < species.Count; i++)
            {
                if (species[i] == false) continue;
                sumWeight += Weights[i];
                sumWorth += Worths[i];
            }

            if (sumWeight > MaxVolume) return 0;
            return sumWorth;
        }
    }
}