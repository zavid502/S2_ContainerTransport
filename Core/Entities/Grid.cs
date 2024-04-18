using Core.Exceptions;

namespace Core.Entities;

public class Grid
{
    public Grid()
    {
        
    }
    public IReadOnlyList<Row> Rows => _rows.AsReadOnly();

    private List<Row> _rows;

    public Row? Row(int row) => _rows.ElementAtOrDefault(row);

    public bool TryPlace(Container container, int row, int column)
    {
        var stack = Row(row)?.Column(column) ?? throw new PositionOutOfBoundsException(row, column);
        return stack.TryPlace(container);
    }
}