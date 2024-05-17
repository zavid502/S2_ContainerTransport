using Core.Exceptions;
using Core.Interfaces;

namespace Core.Entities;

public class Grid
{
    public Grid(int rows, int columns)
    {
        RowsInt = rows;
        ColumnsInt = columns;
        
        _rows = new Row[rows];

        for (int i = 0; i < rows; i++)
        {
            _rows[i] = new(columns);
        }
    }

    public int RowsInt { get; }
    public int ColumnsInt { get; }
    
    public IReadOnlyList<Row> Rows => _rows.AsReadOnly();

    private Row[] _rows;

    public Row? Row(int row) => _rows.ElementAtOrDefault(row);
    
    public List<Stack> EmptyStacks {
        get
        {
            List<Stack> empty = [];
            for (int i = 0; i < RowsInt; i++)
            {
                for (int x = 0; i < ColumnsInt; x++)
                {
                    if (Row(i).Column(x) is not null && Row(i)!.Column(x)!.Empty)
                    {
                        empty.Add(Row(i)!.Column(x)!);
                    }
                }
            }

            return empty;
        }
    }

    public bool TryPlace(IContainer container, int row, int column)
    {
        var stack = Row(row)?.Column(column) ?? throw new PositionOutOfBoundsException(row, column);
        return stack.TryPlace(container);
    }

    public bool WeightRatioCorrectAfterAdd(int row, int col, IContainer container)
    {
        return true;
    }
}