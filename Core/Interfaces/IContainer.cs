namespace Core.Interfaces;

public interface IContainer
{
    bool Cooled { get; }

    public bool Valuable { get; }

    public int ContentWeight { get; set; }

    public int CombinedWeight => this.ContentWeight + this.ContainerWeight;

    public int MaxContentWeight { get; }

    public int MaxWeightOnTop { get; }

    public int ContainerWeight { get; set; }
}