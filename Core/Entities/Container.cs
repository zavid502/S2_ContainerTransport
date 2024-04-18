namespace Core.Entities;

public class Container
{
    public Container(bool cooled = false, bool valuable = false, int contentWeight = 5000, int maxWeightOnTop = 120000)
    {
        this.Cooled = cooled;
        this.Valuable = valuable;
        this.ContentWeight = contentWeight;
        this.MaxWeightOnTop = maxWeightOnTop;
    }

    public bool Cooled { get; private set; }

    public bool Valuable { get; private set; }

    private int ContentWeight { get; set; }

    public int CombinedWeight => this.ContentWeight + this.ContainerWeight;

    public int MaxContentWeight { get; private set; } = 26000;

    public int MaxWeightOnTop { get; private set; }

    private int ContainerWeight { get; set; } = 4000;

}