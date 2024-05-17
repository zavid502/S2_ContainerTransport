using Core.Entities;
using Core.Interfaces;

namespace Core.Helper;

public static class ContainerFactory
{
    public static List<IContainer> GenerateRandomContainers(int amount)
    {
        var list = new List<IContainer>();

        for (int i = 0; i < amount; i++)
        {
            var container = new Container(Random.Shared.NextDouble() > 0.5, Random.Shared.NextDouble() > 0.5, Random.Shared.Next(3000, 6000));
            list.Add(container);
        }

        return list;
    }
}