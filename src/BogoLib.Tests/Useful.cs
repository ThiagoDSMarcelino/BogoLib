using System.Drawing;

namespace BogoLib.Tests;

public static class Useful
{
    public static bool CompareSortedLists<T>(this IEnumerable<T> expected, IEnumerable<T> actual)
        where T : IComparable
        => expected.Zip(actual, (expect, item) => expect.CompareTo(item) == 0).All(test => test);
    
    public static bool CompareConvexHull(this PointF[] expected, PointF[] actual)
    {
        if (expected.Length != actual.Length)
            return false;

        int index = actual.ToList().IndexOf(expected[0]);

        if (index == -1)
            return false;

        var list = RotateList(actual.ToList(), index);
        var inverseList = list.Skip(1).Reverse().ToList();
        inverseList.Insert(0, list[0]);

        var option1 = expected.Zip(list, (expect, item) => expect == item).All(test => test);
        var option2 = expected.Zip(inverseList, (expect, item) => expect == item).All(test => test);

        return option1 || option2;
    }

    private static List<T> RotateList<T>(List<T> list, int positions)
    {
        List<T> temp = new(list);
        int n = list.Count;

        for (int i = 0; i < n; i++)
        {
            list[i] = temp[(i + positions) % n];
        }

        return list;
    }
}
