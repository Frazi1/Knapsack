namespace Knapsack
{
    public interface IGenesCrossover
    {
        Species Produce(Species parent1, Species parent2);
    }
}