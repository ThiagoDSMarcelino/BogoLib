namespace BogoLib;

/// <summary>
/// Represents all available sorting algorithms for BogoSort
/// </summary>
public enum SortingMode : byte
{
    /// <summary>
    /// Shuffle all elements of the collection
    /// </summary>
    Shuffle,

    /// <summary>
    /// Swap two elements of the collection
    /// </summary>
    Swap,

    /// <summary>
    /// Swap two elements of the collection if they are not in order
    /// </summary>
    Checking
}