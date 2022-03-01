using System.Collections.Generic;

namespace Lottery.Util
{
    public static class Collections
    {
        public static int Intersections<T>(this ICollection<T> c1, ICollection<T> c2)
        {
            int count = 0;

            if (c1.IsNullOrEmpty<T>())
            {
                return count;
            }

            foreach (T t in c1)
            {
                if (c2.Contains(t))
                {
                    count++;
                }
            }

            return count;
        }
        
        public static bool IsNullOrEmpty<T>(this ICollection<T> c)
        {
            return (c == null) || (c.Count == 0);
        }
    }
}
