using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PairSum;

namespace PairSumTest
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.AreEqual(3, new[] {1, 1, 2, 1, 1, 0, 1}.GetPairs(2).Count());

            Assert.IsTrue(new [] {1, 2}.Contains(new[] { 1, 1, 2, 1, 1, 0, 1 }.GetPairs(3).Single().Item1));

            Assert.AreEqual(0, new[] { 1, 1, 2, 1, 1, 0, 1 }.GetPairs(4).Count());
            Assert.AreEqual(0, new int[] { }.GetPairs(2).Count());
        }
    }
}