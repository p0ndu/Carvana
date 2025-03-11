using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalAPI.Helpers 
{
    public static class SearchHelper<T>
    {
        // Binary Search
        public static T? BinarySearch(List<T> list, Func<T, IComparable> keySelector, IComparable target)
        {
            int left = 0, right = list.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                var midValue = keySelector(list[mid]);

                int comparison = midValue.CompareTo(target);
                if (comparison == 0)
                    return list[mid];
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return default;
        }

        // Hash 
        public static T? HashSearch(List<T> list, Func<T, IComparable> keySelector, IComparable target)
        {
            var dict = list.ToDictionary(keySelector, item => item);
            return dict.ContainsKey(target) ? dict[target] : default;
        }

        // Ternary 
        public static T? TernarySearch(List<T> list, Func<T, IComparable> keySelector, IComparable target)
        {
            int left = 0, right = list.Count - 1;

            while (left <= right)
            {
                int mid1 = left + (right - left) / 3;
                int mid2 = right - (right - left) / 3;

                var val1 = keySelector(list[mid1]);
                var val2 = keySelector(list[mid2]);

                if (val1.CompareTo(target) == 0) return list[mid1];
                if (val2.CompareTo(target) == 0) return list[mid2];

                if (target.CompareTo(val1) < 0)
                    right = mid1 - 1;
                else if (target.CompareTo(val2) > 0)
                    left = mid2 + 1;
                else
                {
                    left = mid1 + 1;
                    right = mid2 - 1;
                }
            }
            return default;
        }
    }
}
