using BogoLib;

var arr = new int[] { 5, 6, 2, 9, 1 };
var newArr = Bogo<int>.Sort(arr, true);

foreach (var item in arr)
    System.Console.Write($"{item}, ");
System.Console.WriteLine();
foreach (var item in newArr)
    System.Console.Write($"{item}, ");