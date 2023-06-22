using BogoLib;
using System;

var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
var descendingArr = arr.BogoSort(true);

Console.WriteLine(BogoMath.Sqrt(27));
Console.WriteLine(arr.BogoFind(8));
foreach (var item in descendingArr)
    Console.Write($"{item}, ");