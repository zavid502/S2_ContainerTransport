using Core.Entities;

namespace Core.Helper;

public static class ShipFactory
{
    public static Ship GenerateShip()
    {
        var ship = new Ship(6,4);
        return ship;
    }
}