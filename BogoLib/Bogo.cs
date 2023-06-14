namespace BogoLib;

/// <summary>
/// Represents a collection of bogo algorithms 
/// </summary>
public static partial class Bogo
{
    /// <summary>
    /// Returns the first found index of the <paramref name="target" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="array" /></typeparam>
    /// <param name="array">A array of values to order</param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int Find<T>(this T[] arr, T target)
        where T : IComparable
    {
        if (arr.Length == 0)
            throw new ArgumentException("Arr cannot be empty", nameof(arr));

        bool allChecked = false;

        while (!allChecked)
        {
            int i = Shared.Next(arr.Length);
            bool[] indexesUsed = new bool[arr.Length];

            while (indexesUsed[i])
                i = Shared.Next(arr.Length);

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
}