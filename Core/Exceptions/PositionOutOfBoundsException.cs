namespace Core.Exceptions;

public class PositionOutOfBoundsException : Exception
{
    public PositionOutOfBoundsException(int x, int y)
        : base($"Position x{x} y{y} is out of bounds!")
    {
    }
    
    public PositionOutOfBoundsException(int position)
        : base($"Position {position} is out of bounds!")
    {
    }
}