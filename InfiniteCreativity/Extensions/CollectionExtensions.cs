using System.Collections;
using System.Collections.Generic;

namespace Extensions
{
    public static class CollectionExtensions
    {
        public static IList<T> ShuffleInPlace<T>(this IList<T> list, Random rnd)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = rnd.Next(0, n+1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
            return list; 
        }
    }
}