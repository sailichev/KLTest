using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueTest
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.IsTrue(Test(0, 3, 7, 2));
            Assert.IsTrue(Test(1, 3, 7, 2));
            Assert.IsTrue(Test(40000, 3, 7, 3));
            Assert.IsTrue(Test(50000, 1, 17, 4));
            Assert.IsTrue(Test(60000, 13, 1, 5));
        }

        private static bool Test(int itemsCount, int pusherCount, int popperCount, int secondsToWait)
        {
            var input = Enumerable.Range(1, itemsCount);

            int expected = input.Sum();
            int actual = 0;

            using (var q = new ThreadingQueue<int>())
            {
                var tasks = input
                    .GroupBy(_ => _ % pusherCount)
                    .Select(_ => _.ToArray())
                    .Select<int[], Action>(_ => () => { Array.ForEach(_, q.Push); })
                    .Union(Enumerable.Repeat<Action>(() => { while (true) Interlocked.Add(ref actual, q.Pop()); }, popperCount));

                Task.WaitAll(tasks.Select(Task.Factory.StartNew).ToArray(), TimeSpan.FromSeconds(secondsToWait));
            }

            return expected == actual;
        }
    }
}