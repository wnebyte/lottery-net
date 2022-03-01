using Lottery.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Util
{
    [TestClass]
    class RandomGeneratorTest
    {
        private RandomGenerator randGenerator;

        [TestInitialize]
        public void Setup()
        {
            randGenerator = RandomGenerator.Get();
        }

        [TestMethod]
        public void Test00()
        {
            int value = randGenerator.Generate();
            Assert.IsTrue(1 <= value);
            Assert.IsTrue(value <= 35);
        }
    }
}
