using Core.Algorithm;
using Core.Entities;
using Core.Helper;
using Core.Interfaces;

namespace AlgoConsole;

class Program
{
    static void Main(string[] args)
    {
        var ship = ShipFactory.GenerateShip();
        var algorithm = new ContainerSort
        {
            Containers = [
                new Container(false,false),
                new Container(false,false),
                new Container(false,false),
                new Container(false,true),
                new Container(false,false),
                new Container(false,false),
                new Container(false,false),
                new Container(false,true),
                new Container(false,false),
                new Container(false,false),
                new Container(false,false),
                new Container(false,true),
                new Container(false,false),
                new Container(false,false),
                new Container(false,false),
                new Container(false,true)
            ]
        };

        for (int row = 0; row < ship.Grid.RowsInt; row++)
        {
            for (int column = 0; column < ship.Grid.ColumnsInt; column++)
            {
                Console.WriteLine($"\n\nSorted list at {row} {column}");
                Console.WriteLine($"Already present containers: {ship.Grid.Row(row)!.Column(column)!.Containers.Count}\n");
                
                foreach (var container in algorithm.FilteredSort(ship.Grid, row, column))
                {
                    foreach (var field in typeof(Container).GetProperties())
                    {
                        Console.WriteLine($"{field.Name}, {field.GetValue(container)}");
                    }
                    
                    Console.WriteLine();
                    
                    if (ship.Grid.TryPlace(container, row, column))
                    {
                        algorithm.Containers.Remove(container);
                    }
                }
            }
        }
        
        Console.WriteLine("Remaining containers:");
        foreach (var container in algorithm.Containers)
        {
            Console.WriteLine(container.CombinedWeight);
        }

        var url = $"https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length={ship.Grid.RowsInt}&width={ship.Grid.ColumnsInt}&stacks=";
        var stacksString = "";
        var placedContainers = new HashSet<IContainer>();

        for (int row = 0; row < ship.Grid.RowsInt; row++)
        {
            var rowString = "";
            for (int column = 0; column < ship.Grid.ColumnsInt; column++)
            {
                var stack = ship.Grid.Row(row)!.Column(column);
                if (stack?.Containers.Any() == true)
                {
                    foreach (var container in stack.Containers)
                    {
                        if (placedContainers.Add(container))
                        {
                            int containerType;
                            if (!container.Valuable && !container.Cooled)
                                containerType = 1;
                            else if (container.Valuable && !container.Cooled)
                                containerType = 2;
                            else if (!container.Valuable && container.Cooled)
                                containerType = 3;
                            else
                                containerType = 4;

                            rowString += containerType;
                        }
                    }
                }
            }
            stacksString += rowString + (row < ship.Grid.RowsInt - 1 ? "/" : "");
        }

        url += stacksString;
        Console.WriteLine(url);
    }
}