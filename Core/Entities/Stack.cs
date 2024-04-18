using Business;
using Core.Exceptions;

namespace Core.Entities;

public class Stack
{
    public Stack()
    {
    }

    public Stack(IEnumerable<Container> containers)
    {
        this._containers = containers.ToList();
    }

    public bool Empty => _containers.Count == 0;

    public Container? TopContainer => _containers.ElementAtOrDefault(_containers.Count - 1);

    private List<Container> _containers = [];


    public bool TryPlace(Container container)
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