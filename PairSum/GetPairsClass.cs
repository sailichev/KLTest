using System;
using System.Collections.Generic;

namespace PairSum
{
    public static class GetPairsClass
    {
        public static IEnumerable<Tuple<int, int>> GetPairs(this IEnumerable<int> values, int sum)
        {
            var cache = new Dictionary<int, int>();

            foreach (var first in values)
            {
                var second = sum - first;

                if (cache.ContainsKey(second) && cache[second] > 0)
                {
                    yield return new Tuple<int, int>(first, second);

                    cache[second] -= 1;
                }
                else if (cache.ContainsKey(first))
                {
                    cache[first] += 1;
                }
                else
                {
                    cache[first] = 1;
                }
            }
        }
    }
}