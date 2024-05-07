namespace Core.Entities;

public class Ship
{
    public Ship(int rows, int columns)
    {
        Grid = new Grid(rows, columns);
    }
    public double MinCargoWeightPercentage { get; private set; } = 0.5;

    public double MaxLrWeightDiffPercentage { get; private set; } = 0.2;

    public int ContainerLength { get; private set; }

    public int ContainerWidth { get; private set; }

    public Grid Grid { get; init; }
}