using System.Collections.Generic;
using System.Linq;

namespace BogoLib;

/// <summary>
/// Contains bogo algorithms that work with <see cref="IEnumerable{T}"/>
/// </summary>
public static class BogoEnumerable
{
    /// <summary>
    /// Find an element within an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">Any object that inherits from <see cref="IComparable"/></typeparam>
    /// <param name="source"><typeparamref name="T"/> object collection</param>
    /// <param name="target"><typeparamref name="T"/> object that wants to find the index</param>
    /// <returns><paramref name="target"/> index or -1 if it is not found in the <paramref name="source"/></returns>
    public static int BogoFind<T>(this IEnumerable<T> source, T target)
        where T : IComparable
    {
        T[] arr = source.ToArray();
        int n = arr.Length;

        int i = 0;
        bool allChecked = n == 0;
        bool[] indexesUsed = new bool[n];

        while (!allChecked)
        {
            do
            {
                i = Shared.Next(n);
            } while (indexesUsed[i]);

            indexesUsed[i] = true;

            if (arr[i].CompareTo(target) == 0)
                return i;

            allChecked = indexesUsed.All(index => index);
        }

        return -1;
    }

    /// <summary>
    /// Sort the elements of a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">Any object that inherits from <see cref="IComparable"/></typeparam>
    /// <param name="source"><typeparamref name="T"/> object collection</param>
    /// <param name="isDescending">Informs whether the collection should be sorted in descending order</param>
    /// <param name="sortingMode">Informs the bogo sorting algorithm that should be used</param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> BogoSort<T>(this IEnumerable<T> source, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        var arr = source.ToArray();

        while (!arr.IsAscending())
        {
            switch (sortingMode)
            {
                case SortingMode.Shuffle:
                    arr.ShuffleSort();
                    break;

                case SortingMode.Swap:
                    arr.SwapSort();
                    break;

                case SortingMode.Checking:
                    arr.CheckingSortAscending();
                    break;
            }
        }

        return arr.OrderBy(key => 0); // Returns original order of the collection
    }

    public static IOrderedEnumerable<T> BogoSortDescending<T>(this IEnumerable<T> source, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        var arr = source.ToArray();

        while (!arr.IsDescending())
        {
            switch (sortingMode)
            {
                case SortingMode.Shuffle:
                    arr.ShuffleSort();
                    break;

                case SortingMode.Swap:
                    arr.SwapSort();
                    break;

                case SortingMode.Checking:
                    arr.CheckingSortDescending();
                    break;
            }
        }

        return arr.OrderBy(key => 0); // Returns original order of the collection
    }

    private static bool IsAscending<T>(this T[] arr)
        where T : IComparable
    {
        bool isSorted = true;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) == 1)
            {
                isSorted = false;
                break;
            }
        }

        return isSorted;
    }

    private static bool IsDescending<T>(this T[] arr)
        where T : IComparable
    {
        bool isSorted = true;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) == -1)
            {
                isSorted = false;
                break;
            }
        }

        return isSorted;
    }

    private static T[] ShuffleSort<T>(this T[] arr)
    {
        int n = arr.Length;
        while (n > 1)
        {
            int k = Shared.Next(n--);
            SwapItems(ref arr[n], ref arr[k]);
        }

        return arr;
    }

    private static T[] SwapSort<T>(this T[] arr)
    {
        int n = Shared.Next(arr.Length);
        int k = Shared.Next(arr.Length);

        while (n == k)
            k = Shared.Next(arr.Length);

        SwapItems(ref arr[n], ref arr[k]);

        return arr;
    }

    private static T[] CheckingSortAscending<T>(this T[] arr)
        where T : IComparable
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) == 1)
            {
                int randIndex = Shared.Next(i, arr.Length);
                SwapItems(ref arr[i], ref arr[randIndex]);
            }
        }

        return arr;
    }

    private static T[] CheckingSortDescending<T>(this T[] arr)
        where T : IComparable
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) == -1)
            {
                int randIndex = Shared.Next(i, arr.Length);
                SwapItems(ref arr[i], ref arr[randIndex]);
            }
        }

        return arr;
    }

    private static void SwapItems<T>(ref T val1, ref T val2) =>
        (val1, val2) = (val2, val1);
}
