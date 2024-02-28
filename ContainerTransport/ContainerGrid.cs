using System.Text;

namespace ContainerTransport;

public class ContainerGrid
{
    public ContainerGrid(int width, int length)
    {
        Width = width;
        Length = length;
    }
    
    public int Width { get; private set; }
    public int Length { get; private set; }

    private List<Container> _containers = [];
    
    public List<Container> Containers => _containers.ToList();

    public bool FrontOrBackAccessible(int x, int y)
    {
        var front = TryGet(x, y + 1);
        var back = TryGet(x, y - 1);
        
        var frontAccessible = front == null || front.Count == 0;
        var backAccessible = back == null || back.Count == 0;

        return frontAccessible || backAccessible;
    }

    public List<Container>? TryGet(int x, int y)
    {
        if (!WithinBounds(x, y))
        {
            return null;
        }
        return _containers.Where(c => c.X == x && c.Y == y).ToList();
    }

    public bool TryPlace(Container container, int x, int y)
    {
        if (!CanBePlacedAt(container, x, y))
        {
            return false;
        }

        container.X = x;
        container.Y = y;
        container.Z = TryGet(x, y)!.OrderByDescending(c => c.Z).First().Z + 1;
        
        _containers.Add(container);

        return true;
    }
    
    public bool CanBePlacedAt(Container container, int x, int y)
    {
        if (!WithinBounds(x, y))
        {
            return false;
        }
        
        if (AddingExceedsWeight(container, x, y))
        {
            return false;
        }

        if (container.Valuable && !FrontOrBackAccessible(x, y))
        {
            return false;
        }

        var topContainer = TryGetTopContainer(x, y);

        if (topContainer is not null)
        {
            if (topContainer.Valuable)
            {
                return false;
            }
        }

        if (container.Cooled && container.Y != 0)
        {
            return false;
        }

        return true;
    }
    
    private bool WithinBounds(int x, int y)
    {
        return x >= 0 && x <= Width && y >= 0 && y <= Length;
    }

    private Container? TryGetTopContainer(int x, int y)
    {
        var at = TryGet(x, y);
        if (at is null)
        {
            return null;
        }

        return at.OrderByDescending(c => c.Z).First();
    }

    private bool AddingExceedsWeight(Container container, int x, int y)
    {
        var containers = TryGet(x, y);
        if (containers == null)
        {
            return true;
        }
        
        var sum = containers.Select(c => c.CombinedWeight).Sum();

        var exceed = sum + container.CombinedWeight > containers.First(c => c.Z == 0).MaxWeightOnTop;

        return exceed;
    }

    public string ToUrl()
    {
        return $"length={Length}&width={Width}";
    }
}