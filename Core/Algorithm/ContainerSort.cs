using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Algorithm;

public class ContainerSort : IContainerSortingAlgorithm
{
    public List<IContainer> Containers { get; set; } = [];

    public List<IContainer> Sort()
    {
        throw new NotImplementedException();
    }

    public List<IContainer> FilteredSort(Grid grid, int row, int column)
    {
        var containers = Containers.AsEnumerable();
        var stack = grid.Row(row)?.Column(column) ?? throw new PositionOutOfBoundsException(row, column);

        var canPlaceCooled = row == 0;

        if (!canPlaceCooled)
        {
            containers = containers.Where(c => !c.Cooled);
        }

        var front = grid.Row(row - 1)?.Column(column);
        var back = grid.Row(row + 1)?.Column(column);

        var frontOrBackAccessible = (front is not null && front.Empty) || (back is not null && back.Empty);
        var containerBelowValuable = stack.TopContainer is not null && stack.TopContainer.Valuable;
        
        if (!frontOrBackAccessible || containerBelowValuable)
        {
            containers = containers.Where(c => !c.Valuable);
        }
        
        // schip ratio-controle

        containers = containers.OrderByDescending(c => c.MaxWeightOnTop);

        containers = containers.OrderBy(c => c.CombinedWeight);

        containers = containers.OrderBy(c => c.Valuable);

        containers = containers.OrderByDescending(c => c.Cooled);

        return containers.ToList();
    }
}