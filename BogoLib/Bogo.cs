using System;

namespace BogoLib
{
    public static class Bogo
    {
        public static T[] BogoSort<T>(this T[] array, bool descending = false)
            where T : IComparable
        {
            Random random = Random.Shared;
            bool isSorted = false;
            T[] newArray = new T[array.Length];

            array.CopyTo(newArray, 0);

            while (!isSorted)
            {
                int n = newArray.Length;
                while (n > 1) 
                {
                    int k = random.Next(n--);
                    T temp = newArray[n];
                    newArray[n] = newArray[k];
                    newArray[k] = temp;
                }

                isSorted = true;
                for (int i = 0; i < newArray.Length - 1; i++)
                {
                    if (newArray[i].CompareTo(newArray[i+1]) == 1 && !descending)
                    {
                        isSorted = false;
                        break;
                    }

                    if (newArray[i].CompareTo(newArray[i+1]) == -1 && descending)
                    {
                        isSorted = false;
                        break;
                    }
                }
            }
            
            return newArray;
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