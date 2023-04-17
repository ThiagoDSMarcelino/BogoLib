using BogoLib;

var arr = new int[] { 1, 2, 3, 5 };
var newArr = arr.BogoSort(true);

foreach (var item in arr)
    System.Console.Write($"{item}, ");

System.Console.WriteLine();

foreach (var item in newArr)
    System.Console.Write($"{item}, ");

System.Console.WriteLine();

System.Console.WriteLine(arr.BogoFind(6));