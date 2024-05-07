using System.ComponentModel;
using IContainer = Core.Interfaces.IContainer;

namespace Core.Entities;

public class Container : IContainer
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

    public int ContentWeight { get; set; }

    public int CombinedWeight => this.ContentWeight + this.ContainerWeight;

    public int MaxContentWeight { get; private set; } = 26000;

    public int MaxWeightOnTop { get; private set; }

    public int ContainerWeight { get; set; } = 4000;

}