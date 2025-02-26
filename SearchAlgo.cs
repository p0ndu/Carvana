using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SearchAlgorithms<T> where T : IComparable<T>
{
    // Binary
    public static int BinarySearch(List<T> list, T key)
    {
        // List is sorted
        list.Sort(); 
        return BinarySearchRecursive(list, key, 0, list.Count - 1);
    }

    private static int BinarySearchRecursive(List<T> list, T key, int left, int right)
    {
        if (left > right)
            return -1; 

        int mid = left + (right - left) / 2;
        int comparison = list[mid].CompareTo(key);

        if (comparison == 0)
            return mid;
        else if (comparison > 0)
            return BinarySearchRecursive(list, key, left, mid - 1);
        else
            return BinarySearchRecursive(list, key, mid + 1, right);
    }

    // Ternary Search
    public static int TernarySearch(List<T> list, T key)
    {
        list.Sort();
        return TernarySearchRecursive(list, key, 0, list.Count - 1);
    }

    private static int TernarySearchRecursive(List<T> list, T key, int left, int right)
    {
        if (left > right)
            return -1;

        int mid1 = left + (right - left) / 3;
        int mid2 = right - (right - left) / 3;

        if (list[mid1].CompareTo(key) == 0)
            return mid1;
        if (list[mid2].CompareTo(key) == 0)
            return mid2;

        if (key.CompareTo(list[mid1]) < 0)
            return TernarySearchRecursive(list, key, left, mid1 - 1);
        else if (key.CompareTo(list[mid2]) > 0)
            return TernarySearchRecursive(list, key, mid2 + 1, right);
        else
            return TernarySearchRecursive(list, key, mid1 + 1, mid2 - 1);
    }

    // Hash Search
    public static bool HashSearch<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key, out TValue value)
    {
        return dict.TryGetValue(key, out value);
    }
}

