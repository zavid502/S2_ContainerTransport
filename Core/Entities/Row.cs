using Core.Exceptions;

namespace Core.Entities;

public class Row
{
    public Row(int length)
    {
        this.Length = length;
        this._stacks = Enumerable.Repeat(new Stack(), length).ToList();
    }


    public int Length { get; init; }

    public IReadOnlyList<Stack> Stacks => _stacks;

    private readonly IReadOnlyList<Stack> _stacks;

    public bool TryPlace(Container container, int position)
    {
        var stack = _stacks.ElementAt(position) ?? throw new PositionOutOfBoundsException(position);

        return stack.TryPlace(container);
    }

    public Stack? Column(int column) => _stacks.ElementAtOrDefault(column);
}