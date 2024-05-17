using Business;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Entities;

public class Stack
{
    public Stack()
    {
    }

    public Stack(IEnumerable<IContainer> containers)
    {
        this._containers = containers.ToList();
    }

    public bool Empty => _containers.Count == 0;

    public IContainer? TopContainer => _containers.ElementAtOrDefault(_containers.Count - 1);

    private List<IContainer> _containers = [];

    public IReadOnlyCollection<IContainer> Containers => _containers.AsReadOnly();


    public bool TryPlace(IContainer container)
    {
        if (!ContainerCompatibilityChecks.WeightCompatible(_containers, container))
        {
            return false;
        }

        if (!ContainerCompatibilityChecks.ContainerNotPlacedOnValuableContainer(this))
        {
            return false;
        }

        _containers.Add(container);

        return true;
    }
}