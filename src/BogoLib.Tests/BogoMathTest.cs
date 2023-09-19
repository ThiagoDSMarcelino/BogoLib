using System.Drawing;

namespace BogoLib.Tests;

public class BogoMathTest
{
    [Fact]
    public void SquareTest()
    {
        var points = new PointF[]
        {
            new(0, 0), new(0, 5),
            new(5, 5), new(5, 0)
        };

        var result = points.BogoConvexHull();

        var correct = new PointF[]
        {
            new(0, 0), new(0, 5),
            new(5, 5), new(5, 0)
        };
        
        Assert.True(correct.CompareConvexHull(result));
    }

    [Fact]
    public void CastleTest()
    {
        var points = new PointF[]
        {
            new(1, 1), new(1, 5),
            new(2, 6), new(3, 4),
            new(4, 6), new(5, 5),
            new(5, 1)
        };

        var result = points.BogoConvexHull();
        
        var correct = new PointF[]
        {
            new(1, 1), new(5, 1),
            new(5, 5), new(4, 6),
            new(2, 6), new(1, 5)
        };

        Assert.True(correct.CompareConvexHull(result));
    }

    [Fact]
    public void FinalTest()
    {
        var points = new PointF[]
        {
            new(1, 0), new(3.5f, 5),
            new(1, 5), new(3, 1),
            new(1.7f, -3)
        };

        var result = points.BogoConvexHull();
        
        var correct = new PointF[]
        {
            new(1.7f, -3), new(3, 1),
            new(3.5f, 5), new(1, 5),
            new(1, 0)
        };

        Assert.True(correct.CompareConvexHull(result));
    }
}
