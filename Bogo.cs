using System;

namespace BogoLib
{
    public static class Bogo<T>
        where T : IComparable
    {
        public static T[] Sort(T[] array, bool descending = false, Random rand = null)
        {
            if (rand is null)
                rand = new Random();

            bool isSorted = false;
            T[] newArray = new T[array.Length];
            array.CopyTo(newArray, 0);

            while (!isSorted)
            {
                int n = newArray.Length;
                while (n > 1) 
                {
                    int k = rand.Next(n--);
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
    }
}