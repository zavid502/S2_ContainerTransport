namespace ContainerTransport;

public class Container (int x = 0, int y = 0, int z = 0)
{
    public bool Cooled { get; private set; }
    public bool Valuable { get; private set; }
    public int ContentWeight { get; private set; }
    public int ContainerWeight { get; private set; } = 4000;
    public int CombinedWeight => ContentWeight + ContainerWeight;
    public int MaxContentWeight { get; private set; } = 26000;
    public int MaxWeightOnTop { get; private set; } = 120000;
    
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Z { get; set; } = z;
    
}