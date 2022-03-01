using System;

namespace Lottery.Util
{
    public class RandomGenerator : IRandomGenerator
    {
        private static RandomGenerator INSTANCE;

        public readonly int lower;

        public readonly int upper;

        private readonly Random rand;

        public static RandomGenerator Get(int lower = 1, int upper = 35)
        {
            if (INSTANCE == null)
            {
                INSTANCE = new RandomGenerator(lower, upper);
            }

            return INSTANCE;
        }

        private RandomGenerator(int lower, int upper)
        {
            this.lower = lower;
            this.upper = upper;
            this.rand = new Random();
        }

        public int Generate()
        {
            int value = rand.Next(lower, upper + 1);
            return value;
        }

        public override string ToString()
        {
            return string.Format(
                    "Lottery.Util.RandomGenerator[lower: {0}, upper: {1}]", lower, upper
                );
        }
    }
}
