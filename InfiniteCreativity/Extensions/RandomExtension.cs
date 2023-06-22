namespace InfiniteCreativity.Extensions
{
    public static class RandomExtension
    {
        public static T Next<T>(this Random random, List<T> items)
        {
            return items[random.Next(0, items.Count)];
        }

        public static double NextDouble(this Random random, double min, double max)
        {
            return (random.NextDouble() * (max - min)) + min;
        }
    }
}
