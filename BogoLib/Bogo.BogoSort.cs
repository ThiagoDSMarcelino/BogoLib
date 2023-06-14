using System.Collections.Generic;

namespace BogoLib;

public static partial class Bogo
{
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
    public static ICollection<T> Sort<T>(this ICollection<T> source, bool isDescending = false, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        var arr = new T[source.Count];
        source.CopyTo(arr, 0);

        bool isSorted = false;

        while (!isSorted)
        {
            isSorted = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending)
                {
                    isSorted = false;
                    break;
                }

                if (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending)
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