using Business;
using Core.Entities;
using Core.Interfaces;

namespace Tests;

public class Tests
{
    [Test]
    public void ContainerCanSupportWeight_WithoutWeightOverflow_ReturnsTrue()
    {
        List<IContainer> containers = [
            new Container(),
            new Container(),
            new Container(),
            new Container()
        ];

        var aboveOverflow = ContainerCompatibilityChecks.ContainerCanSupportWeight(containers, 2);
        
        Assert.That(aboveOverflow, Is.True);
    }

    [Test]
    public void ContainerCanSupportWeight_WithWeightOverflow_ReturnsFalse()
    {
        List<IContainer> containers = [
            new Container(),
            new Container(),
            new Container(false, false, 120000),
        ];

        var aboveOverflow = ContainerCompatibilityChecks.ContainerCanSupportWeight(containers, 2);
        
        Assert.That(aboveOverflow, Is.False);
    }

    [Test]
    public void WeightCompatible_WithContainerAddResultingInOverflow_ReturnsFalse()
    {
        List<IContainer> containers = [
            new Container(),
            new Container(false, false, 30000),
            new Container(false, false, 30000),
            new Container(false, false, 30000)
        ];

        var containerToAdd = new Container(false, false, 30000);

        var weightCompatible = ContainerCompatibilityChecks.WeightCompatible(containers, containerToAdd);
        
        Assert.That(weightCompatible, Is.False);
    }
    
    [Test]
    public void WeightCompatible_WithContainerAddNotResultingInOverflow_ReturnsTrue()
    {
        List<IContainer> containers = [
            new Container(),
            new Container(false, false, 30000),
            new Container(false, false, 30000),
            new Container(false, false, 30000)
        ];

        var containerToAdd = new Container(false, false, 10000);

        var weightCompatible = ContainerCompatibilityChecks.WeightCompatible(containers, containerToAdd);
        
        Assert.That(weightCompatible, Is.True);
    }
}