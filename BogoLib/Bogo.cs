using System;
using System.Runtime.InteropServices;

namespace BogoLib
{
    public static class Bogo
    {
        public enum BogoSortType
        {
            Shuffle,
            OneByOne,
            Checking
        }

        public static T[] BogoSort<T>(this T[] array, bool descending = false, BogoSortType type = BogoSortType.Shuffle)
            where T : IComparable
        {
            T[] result = (T[])array.Clone();

            switch (type)
            {
                case BogoSortType.Shuffle:
                    result = result.Shuffle(descending);
                    break;
                
                case BogoSortType.OneByOne:
                    throw new NotImplementedException();
                
                case BogoSortType.Checking:
                    throw new NotImplementedException();
            }

            return result;
        }

        private static T[] Shuffle<T>(this T[] array, bool descending = false)
            where T : IComparable
        {
            Random random = Random.Shared;
            bool isSorted = false;

            while (!isSorted)
            {
                int n = array.Length;
                while (n > 1)
                {
                    int k = random.Next(n--);
                    (array[k], array[n]) = (array[n], array[k]);
                }

                isSorted = true;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i].CompareTo(array[i + 1]) == 1 && !descending)
                    {
                        isSorted = false;
                        break;
                    }

                    if (array[i].CompareTo(array[i + 1]) == -1 && descending)
                    {
                        isSorted = false;
                        break;
                    }
                }
            }

            return array;
        }

        public static int BogoFind<T>(this T[] array, T target)
            where T : IComparable
        {
            if (array.Length == 0)
                throw new Exception();

            Random random = Random.Shared;

            bool[] usedIndex = new bool[array.Length];
            bool allChecked = false;

            while (!allChecked)
            {
                int index = random.Next(array.Length);

                while (usedIndex[index])
                    index = random.Next(array.Length);
                
                usedIndex[index] = true;

                if (array[index].CompareTo(target) == 0)
                    return index;
                
                allChecked = true;
                foreach (bool item in usedIndex)
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