using Core.Algorithm;
using Core.Entities;
using Core.Helper;

namespace Tests;

public class AlgorithmTests
{
    [Test]
    public void ContainerSort_WithVariousContainers_SortsCorrectly()
    {
        var algorithm = new ContainerSort();
        var ship = ShipFactory.GenerateShip();
        
        var valuable = new Container(valuable:true);
        var cooled = new Container(cooled: true);
        
        algorithm.Containers.Add(valuable);
        algorithm.Containers.Add(cooled);

        var sortWithCooled = algorithm.FilteredSort(ship.Grid, 0, 1);
        var sortWithoutCooled = algorithm.FilteredSort(ship.Grid, 1, 1);
    }
}