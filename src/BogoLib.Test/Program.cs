using BogoLib;
using System;
using System.Drawing;
using System.Text;

StringBuilder sb = new();

// ConvexHull Test
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

var test = points.BogoConvexHull();
sb.Append("ConvexHull = { ");
foreach (var point in test)
    sb.Append($"({point.X}, {point.Y}); ");
sb.Append("} n = " + test.Length + '\n');

// Sorting Test
var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
var sortedArr = arr.BogoSort(true);
sb.Append("\nSorted array:\n");
foreach (var item in sortedArr)
    sb.Append($"{item}, ");


// Square root Test
sb.Append($"\n\nSquare root of 27 = {BogoMath.BogoSqrt(27)}\n");

// Find Test
sb.Append($"\n\nIndex of number 9 in the sorted array: {sortedArr.BogoFind(9)}");

Console.WriteLine(sb.ToString());