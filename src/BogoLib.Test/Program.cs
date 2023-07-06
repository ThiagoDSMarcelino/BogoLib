using BogoLib;
using System;
using System.Drawing;
using System.Text;

var points = new PointF[]
{
    // Square Test
    //new PointF(0, 0), new PointF(0, 5),
    //new PointF(5, 5), new PointF(5, 0)
    
    // Castle Test
    new PointF(1, 1), new PointF(1, 5),
    new PointF(2, 6), new PointF(3, 4),
    new PointF(4, 6), new PointF(5, 5),
    new PointF(5, 1)

    // ConvexHull Final test
    //new PointF(1, 0), new PointF(3.5f, 5),
    //new PointF(1, 5), new PointF(3, 1),
    //new PointF(1.7f, -3)
};

StringBuilder sb = new();

var test = points.BogoConvexHull();

sb.Append("\n\nConvexHull = { ");
foreach (var point in test)
    sb.Append($"({point.X}, {point.Y}); ");

sb.Append("} n = " + test.Length);

sb.Append("\n\n\n");

Console.WriteLine(sb.ToString());

// Sorting Test
//var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//var descendingArr = arr.BogoSort(true);

// Square root Test
//Console.WriteLine(BogoMath.BogoSqrt(27));

// Find Test
//Console.WriteLine(arr.BogoFind(8));
//foreach (var item in descendingArr)
//    Console.Write($"{item}, ");