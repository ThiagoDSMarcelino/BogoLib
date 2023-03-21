using BogoLib;

var arr = new int[] { 1, 2, 3, 5 };
// var newArr = Bogo<int>.Sort(arr, true);

// foreach (var item in arr)
//     System.Console.Write($"{item}, ");
// System.Console.WriteLine();
// foreach (var item in newArr)
//     System.Console.Write($"{item}, ");

System.Console.WriteLine( Bogo<int>.Find(arr, 6));