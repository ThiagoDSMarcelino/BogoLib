using System.Collections.Generic;
using System.Collections;
using System;

namespace BogoLib.Linq;

public partial class BogoEnumerable<T> : IEnumerable<T>
{
    private readonly T[] Values = new T[10];
    private readonly int Index = 0;
    public int Length => Index;
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Index)
                throw new IndexOutOfRangeException();
            return Values[index];
        }
        set
        {
            if (index < 0 || index >= Index)
                throw new IndexOutOfRangeException();
            Values[index] = value;
        }
    }

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
        throw new NotImplementedException();
}