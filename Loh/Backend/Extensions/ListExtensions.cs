using System;
using System.Collections.Generic;
using System.Linq;

namespace Loh.Backend.Extensions
{
    public static class ListExtensions
    {
        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            return list[random.Next(list.Count)];
        }

        public static IList<T> TakeAndDelete<T>(this IList<T> list, int count)
        {
            var res = list.Take(count).ToList();

            foreach (var t in res)
                list.Remove(t);

            return res;
        }
    }
}
