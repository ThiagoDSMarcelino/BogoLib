namespace BogoLib.Tests;

public class BogoEnumerableTest
{
    [Fact]
    public void Shuffle()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.Order();
        var bogoSortedList = list.BogoSort();

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }

    [Fact]
    public void ShuffleDescending()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.OrderDescending();
        var bogoSortedList = list.BogoSortDescending();

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }
    
    [Fact]
    public void OneByOne()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.Order();
        var bogoSortedList = list.BogoSort(SortingMode.OneByOne);

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }

    [Fact]
    public void OneByOneDescending()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.OrderDescending();
        var bogoSortedList = list.BogoSortDescending(SortingMode.OneByOne);

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }

    [Fact]
    public void Checking()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.Order();
        var bogoSortedList = list.BogoSort(SortingMode.Checking);

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }

    [Fact]
    public void CheckingDescending()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var sortedList = list.OrderDescending();
        var bogoSortedList = list.BogoSortDescending(SortingMode.Checking);

        Assert.True(sortedList.CompareSortedLists(bogoSortedList));
    }

}