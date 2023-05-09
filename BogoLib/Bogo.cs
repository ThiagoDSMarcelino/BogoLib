using System;
using System.Diagnostics.CodeAnalysis;

namespace BogoLib
{
    public static class Bogo
    {
        public enum SortingType
        {
            Shuffle,
            OneByOne,
            Checking
        }

        public static T[] BogoSort<T>(this T[] array, bool descending = false, bool inplace = false, SortingType sortingType = SortingType.Shuffle)
            where T : IComparable
        {
            T[] newArr = inplace ? array : (T[])array.Clone();

            Random rand = Random.Shared;
            bool isSorted = false;

            while (!isSorted)
            {
                switch (sortingType)
                {
                    case SortingType.Shuffle:
                        newArr.Shuffle(rand);
                        break;

                    case SortingType.OneByOne:
                        newArr.OneByOne(rand);
                        break;

                    case SortingType.Checking:
                        throw new NotImplementedException();

                    default:  throw new ArgumentException();
                }

                isSorted = true;
                for (int i = 0; i < newArr.Length - 1; i++)
                {
                    if (newArr[i].CompareTo(newArr[i + 1]) == 1 && !descending)
                    {
                        isSorted = false;
                        break;
                    }

                    if (newArr[i].CompareTo(newArr[i + 1]) == -1 && descending)
                    {
                        isSorted = false;
                        break;
                    }
                }
            }

            return newArr;
        }

        private static T[] Shuffle<T>(this T[] arr, Random rand)
        {
            int n = arr.Length;
            while (n > 1)
            {
                int k = rand.Next(n--);
                (arr[k], arr[n]) = (arr[n], arr[k]);
            }

            return arr;
        }

        private static T[] OneByOne<T>(this T[] arr, Random rand)
        {
            int n = rand.Next(arr.Length);
            int k = rand.Next(arr.Length);

            while (n == k)
                k = rand.Next(arr.Length);

            (arr[k], arr[n]) = (arr[n], arr[k]);

            return arr;
        }

        public static int BogoFind<T>(this T[] arr, T target)
            where T : IComparable
        {
            if (arr.Length == 0) throw new ArgumentException();

            Random rand = Random.Shared;

            bool[] indexesUsed = new bool[arr.Length];
            bool allChecked = false;

            while (!allChecked)
            {
                int i = rand.Next(arr.Length);

                while (indexesUsed[i])
                    i = rand.Next(arr.Length);

                indexesUsed[i] = true;

                if (arr[i].CompareTo(target) == 0)
                    return i;

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
}