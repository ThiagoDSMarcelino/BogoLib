using System.Collections.Generic;
using System.Linq;

namespace BogoLib;

/// <summary>
/// Contains bogo algorithms that work with <see cref="ICollection{T}"/>
/// </summary>
public static partial class BogoCollection
{
    /// <summary>
    /// Find an element within an <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T">Any object that inherits from <see cref="IComparable"/></typeparam>
    /// <param name="source"><typeparamref name="T"/> object collection</param>
    /// <param name="target"><typeparamref name="T"/> object that wants to find the index</param>
    /// <returns><paramref name="target"/> index or -1 if it is not found in the <paramref name="source"/></returns>
    public static int BogoFind<T>(this ICollection<T> source, T target)
        where T : IComparable
    {
        int n = source.Count;

        var arr = source.CollectionToArray();

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
    /// Sort the elements of a <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T">Any object that inherits from <see cref="IComparable"/></typeparam>
    /// <param name="source"><typeparamref name="T"/> object collection</param>
    /// <param name="isDescending">Informs whether the collection should be sorted in descending order</param>
    /// <param name="sortingMode">Informs the bogo sorting algorithm that should be used</param>
    /// <returns></returns>
    public static ICollection<T> BogoSort<T>(this ICollection<T> source, bool isDescending = false, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        var arr = source.CollectionToArray();

        bool isSorted = false;

        while (!isSorted)
        {
            isSorted = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if ((arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending) ||
                    (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending))
                {
                    isSorted = false;
                    break;
                }
            }

            if (isSorted)
                continue;

            switch (sortingMode)
            {
                case SortingMode.Shuffle:
                    arr.Shuffle();
                    break;

                case SortingMode.OneByOne:
                    arr.OneByOne();
                    break;

                case SortingMode.Checking:
                    arr.Checking(isDescending);
                    break;
            }
        }

        return arr;
    }

    /// <summary>
    /// Represents all available sorting algorithms for BogoSort
    /// </summary>
    public enum SortingMode : byte
    {
        Shuffle,
        OneByOne,
        Checking
    }

    public static T[] CollectionToArray<T>(this ICollection<T> source)
    {
        var result = new T[source.Count];
        source.CopyTo(result, 0);

        return result;
    }

    private static T[] Shuffle<T>(this T[] arr)
    {
        int n = arr.Length;
        while (n > 1)
        {
            int k = Shared.Next(n--);
            Swap(ref arr[n], ref arr[k]);
        }

        return arr;
    }

    private static T[] OneByOne<T>(this T[] arr)
    {
        int n = Shared.Next(arr.Length);
        int k = Shared.Next(arr.Length);

        while (n == k)
            k = Shared.Next(arr.Length);

        Swap(ref arr[n], ref arr[k]);

        return arr;
    }

    private static T[] Checking<T>(this T[] arr, bool isDescending)
        where T : IComparable
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if ((arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending) ||
                (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending))
            {
                int randIndex = Shared.Next(i, arr.Length);
                Swap(ref arr[i], ref arr[randIndex]);
            }
        }

        return arr;
    }

    public static void Swap<T>(ref T val1, ref T val2) =>
        (val1, val2) = (val2, val1);
}