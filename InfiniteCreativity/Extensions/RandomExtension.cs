﻿namespace InfiniteCreativity.Extensions
{
    public static class RandomExtension
    {
        public static T? Next<T>(this Random random, List<T> items)
        {
            return items.Count == 0 ? default(T) : items[random.Next(0, items.Count)];
        }

        public static double NextDouble(this Random random, double min, double max)
        {
            return (random.NextDouble() * (max - min)) + min;
        }

        public static TimeSpan NextTimeDuration(this Random random, int min, int max)
        { 
            var duration = random.Next(min,max);
            return TimeSpan.FromMinutes(duration);
        }
    }
}
