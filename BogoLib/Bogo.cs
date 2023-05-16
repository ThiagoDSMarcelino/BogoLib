using System;

namespace BogoLib;

/// <summary>
/// Represents a collection of bogo algorithms 
/// </summary>
public static class Bogo
{
    /// <summary>
    /// Represents all sorting modes available for BogoSort
    /// </summary>
    public enum SortingMode : byte
    {
        Shuffle,
        OneByOne,
        Checking
    }

    /// <summary>
    /// Sorts the elements in an entire <see cref="Array" /> using the <see cref="IComparable{in T}" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="arr" /></typeparam>
    /// <param name="arr">A array of values to be sorted</param>
    /// <param name="isDescending">Indicates whether the array should be sorted in descending order</param>
    /// <param name="inplace">Indicates whether the font should be modified</param>
    /// <param name="sortingMode">Indicates which BogoSorting mode should be used</param>
    /// <returns>
    /// Sorted <see cref="Array" /> of <typeparamref name="T"> nothing if <paramref name="inplace" /> parameter is true
    /// </returns>
    public static T[] BogoSort<T>(this T[] arr, bool isDescending = false, bool inplace = false, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        T[] newArr = inplace ? arr : (T[])arr.Clone();
        bool isSorted = false;

        while (!isSorted)
        {
            isSorted = true;
            for (int i = 0; i < newArr.Length - 1; i++)
            {
                if (newArr[i].CompareTo(newArr[i + 1]) == 1 && !isDescending)
                {
                    isSorted = false;
                    break;
                }

                if (newArr[i].CompareTo(newArr[i + 1]) == -1 && isDescending)
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
                    newArr.Shuffle();
                    break;

                case SortingMode.OneByOne:
                    newArr.OneByOne();
                    break;

                case SortingMode.Checking:
                    newArr.Checking(isDescending);
                    break;
            }
        }

        return newArr;
    }

    private static T[] Shuffle<T>(this T[] arr)
    {
        int n = arr.Length;
        while (n > 1)
        {
            int k = Random.Shared.Next(n--);
            arr.Swap(n, k);
        }

        return arr;
    }

    private static T[] OneByOne<T>(this T[] arr)
    {
        int n = Random.Shared.Next(arr.Length);
        int k = Random.Shared.Next(arr.Length);

        while (n == k)
            k = Random.Shared.Next(arr.Length);

        arr.Swap(n, k);

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
                int newIndex = Random.Shared.Next(i, arr.Length);
                arr.Swap(i, newIndex);
            }
        }

        return arr;
    }

    private static void Swap<T>(this T[] arr, int index1, int index2) =>
        (arr[index1], arr[index2]) = (arr[index2], arr[index1]);

    /// <summary>
    /// Returns the first found index of the <paramref name="target" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="array" /></typeparam>
    /// <param name="array">A array of values to order</param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int BogoFind<T>(this T[] arr, T target)
        where T : IComparable
    {
        if (arr.Length == 0)
            throw new ArgumentException();

        bool allChecked = false;

        while (!allChecked)
        {
            int i = Random.Shared.Next(arr.Length);
            bool[] indexesUsed = new bool[arr.Length];

            while (indexesUsed[i])
                i = Random.Shared.Next(arr.Length);

            indexesUsed[i] = true;

            if (arr[i].CompareTo(target) == 0) return i;

            allChecked = true;
            foreach (bool item in indexesUsed)
            {
                if (!item)
                {
                    allChecked = false;
                    break;
                }
            }
        }

        return -1;
    }

    // TODO Processor Division Algorithm
}