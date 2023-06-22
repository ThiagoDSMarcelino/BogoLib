using System.Collections.Generic;
using System.Linq;

namespace BogoLib;

/// <summary>
/// Represents a collection of bogo algorithms 
/// </summary>
public static partial class BogoCollection
{
    /// <summary>
    /// Returns the first found index of the <paramref name="target" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="array" /></typeparam>
    /// <param name="array">A array of values to order</param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int BogoFind<T>(this ICollection<T> source, T target)
        where T : IComparable
    {
        int n = source.Count;

        if (n == 0)
            throw new ArgumentException("Arr cannot be empty", nameof(source));

        var arr = source.CollectionToArray();

        int i = 0;
        bool allChecked = false;
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
    /// Sorts the elements in an entire <see cref="Array" /> using the <see cref="IComparable{in T}" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="arr" /></typeparam>
    /// <param name="arr">A array of values to be sorted</param>
    /// <param name="isDescending">Indicates whether the array should be sorted in descending order</param>
    /// <param name="inPlace">Indicates whether the font should be modified</param>
    /// <param name="sortingMode">Indicates which BogoSorting mode should be used</param>
    /// <returns>
    /// Sorted <see cref="Array" /> of <typeparamref name="T"> nothing if <paramref name="inPlace" /> parameter is true
    /// </returns>
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
    /// Represents all sorting modes available for BogoSort
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