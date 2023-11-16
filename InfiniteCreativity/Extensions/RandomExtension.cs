namespace InfiniteCreativity.Extensions
{
    public static class RandomExtension
    {
        public static T? Next<T>(this Random random, IList<T> items)
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

        public static int NextCrit(this Random random, double critChance)
        {
            var guaranteed = (int)critChance;
            var weight = critChance - guaranteed;
            var roll = random.NextDouble();
            return roll < weight ? guaranteed + 1 : guaranteed;
        }

        public static T Next<T>(this Random random) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            return random.Next(values);
        }
    }
}
