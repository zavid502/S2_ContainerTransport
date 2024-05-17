using Core.Entities;
using Core.Interfaces;

namespace Business;

public static class ContainerCompatibilityChecks
{
    public static bool ContainerCanSupportWeight(IList<IContainer> containers, int containerToCheck)
    {
        var toTake = containers.Count - containerToCheck;
        if (toTake <= 0)
        {
            return false;
        }
        var containersOnTop = containers.Reverse().Take(toTake).ToList();
        
        var weightSum = containersOnTop.Sum(c => c.CombinedWeight);
        
        var weightOverflow = weightSum > containers[containerToCheck].MaxWeightOnTop;

        return !weightOverflow;
    }
    
    public static bool WeightCompatible(IList<IContainer> containers, IContainer containerToAdd)
    {
        containers.Add(containerToAdd);

        for (int i = 1; i < containers.Count; i++)
        {
            if (!ContainerCanSupportWeight(containers, i))
            {
                return false;
            }
        }

        return true;
    }

    public static bool ContainerNotPlacedOnValuableContainer(Stack stack)
    {
        return stack.TopContainer!.Valuable;
    }

    public static bool ContainerAccessibleFromFrontOrBack(Row row, int column)
    {
        var back = row.Column(column - 1);
        var front = row.Column(column + 1);

        return back is not null && back.Empty || front is not null && front.Empty;
    }

    public static bool ContainerHasAccessToPower(Grid grid, int row)
    {
        return row == 1;
    }
}