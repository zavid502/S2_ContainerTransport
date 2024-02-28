using ContainerTransport;

var grid = new ContainerGrid(999, 3);

grid.TryPlace(1, 1);
grid.TryPlace(1, 2);
grid.TryPlace(1, 2);

Console.WriteLine(grid.ToUrl());