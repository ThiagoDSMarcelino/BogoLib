using System;

namespace BogoLib
{
    /// <summary>
    /// Collection of bogo algorithms 
    /// </summary>
    public static class Bogo
    {
        /// <summary>
        /// Represents all sorting modes available
        /// </summary>
        public enum SortingMode: byte
        {
            Shuffle,
            OneByOne,
            Checking
        }

        /// <summary>
        /// Sorts the elements of a sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">Sequence to be sorted</param>
        /// <param name="isDescending">True if the sequence should be sorted descending</param>
        /// <param name="inplace"></param>
        /// <param name="sortingMode"></param>
        /// <returns>An <c>array</c> whose the elements are sorted</returns>
        /// <exception cref="ArgumentException">Invalid SortingType</exception>
        public static T[] BogoSort<T>(this T[] arr, bool isDescending = false, bool inplace = false, SortingMode sortingMode = SortingMode.Shuffle)
            where T : IComparable
        {
            T[] newArr = inplace ? arr : (T[])arr.Clone();

            Random rand = Random.Shared;
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
                        newArr.Shuffle(rand);
                        break;

                    case SortingMode.OneByOne:
                        newArr.OneByOne(rand);
                        break;

                    case SortingMode.Checking:
                        newArr.Checking(rand, isDescending);
                        break;

                    default: throw new ArgumentException();
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
                (arr[n], arr[k]) = (arr[k], arr[n]);
            }

            return arr;
        }

        private static T[] OneByOne<T>(this T[] arr, Random rand)
        {
            int n = rand.Next(arr.Length);
            int k = rand.Next(arr.Length);

            while (n == k)
                k = rand.Next(arr.Length);

            (arr[n], arr[k]) = (arr[k], arr[n]);

            return arr;
        }

        private static T[] Checking<T>(this T[] arr, Random rand, bool isDescending)
            where T : IComparable
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {                
                if ((arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending) ||
                    (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending))
                {
                    int newIndex = rand.Next(i, arr.Length);
                    (arr[i], arr[newIndex]) = (arr[newIndex], arr[i]);
                }
            }

            return arr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int BogoFind<T>(this T[] arr, T target)
            where T : IComparable
        {
            if (arr.Length == 0)
                throw new ArgumentException();

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