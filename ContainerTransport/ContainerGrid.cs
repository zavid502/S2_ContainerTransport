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

    public bool TryPlace(int x, int y, Container? container = null)
    {
        var first = TryGet(x, y)!.MaxBy(c => c.Z);

        Container newContainer;

        if (container is null)
        {
            newContainer = new Container
            {
                X = x,
                Y = y,
                Z = first is null ? 0 : first.Z + 1
            };
        }
        else
        {
            newContainer = new Container(x, y, first is null ? 0 : first.Z + 1, container.Cooled,
                container.Valuable, container.ContentWeight);
        }

        if (!CanBePlacedAt(newContainer, x, y))
        {
            return false;
        }

        _containers.Add(newContainer);

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

        return at.MaxBy(c => c.Z);
    }

    private bool AddingExceedsWeight(Container container, int x, int y)
    {
        var containers = TryGet(x, y);
        if (containers == null)
        {
            return true;
        }

        var weightSelect = containers.Select(c => c.CombinedWeight);

        if (!weightSelect.Any())
        {
            return false;
        }

        var sum = weightSelect.Sum();

        var exceed = sum + container.CombinedWeight > containers.First(c => c.Z == 0).MaxWeightOnTop;

        return exceed;
    }

    private string StacksToString()
    {
        string a = "";
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                var get = TryGet(x, y);
                for (int i = 0; i < get!.Count; i++)
                {
                    a += ContainerToNum(get[i]);
                }
                
                if (y != Length - 1)
                {
                    a += ",";
                }
            }

            if (x != Width - 1)
            {
                a += "/";
            }
            
        }

        return a;
    }

    public string ToUrl()
    {
        var value = StacksToString();
        var website = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?";
        return $"{website}length={Length}&width={Width}&stacks={value}";
    }

    private string ContainerToNum(Container container)
    {
        if (container is { Valuable: true, Cooled: true })
        {
            return "4";
        }
        if (container is { Valuable: true, Cooled: false })
        {
            return "3";
        }
        if (container is { Valuable: false, Cooled: true })
        {
            return "2";
        }

        return "1";
    }
}