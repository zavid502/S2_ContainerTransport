namespace ContainerTransport;

public class Ship
{
    public double MinCargoWeightPercentage { get; private set; } = 0.5;
    public double MaxLrWeightDiffPercentage { get; private set; } = 0.2;
    public int ContainerLength { get; private set; }
    public int ContainerWidth { get; private set; }
}