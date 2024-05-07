using System.Diagnostics.Contracts;
using Core.Entities;

namespace Core.Interfaces;

public interface IContainerSortingAlgorithm
{
    List<IContainer> Containers { get; set; }
    List<IContainer> Sort();
    List<IContainer> FilteredSort(Grid grid, int row, int column);
}