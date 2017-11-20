using Data.Pairs;
using FakeItEasy;
using Goldbach.Data.Primes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Data.Pairs
{
    [TestFixture]
    public class GoldbachPairCalculatorTests
    {
        private IPrimeNumberCache TestCache
        {
            get
            {
                var testCache = A.Fake<IPrimeNumberCache>();

                A.CallTo(() => testCache.GetPrimesTo(5)).Returns(new List<int> { 2, 3, 5 });
                A.CallTo(() => testCache.GetPrimesTo(10)).Returns(new List<int> { 2, 3, 5, 7 });

                return testCache;
            }
        }
        
        [Test]
        public void GetPairs_NotAll_ReturnsListContainingFirstValidPair()
        {
            var expectedPairList = new List<Tuple<int, int>> { new Tuple<int, int>(3, 7) };

            var testCalculator = new GoldbachPairCalculator(TestCache);
            var calculatedPairList = testCalculator.GetPairsFor(10).ToList();

            Assert.AreEqual(expectedPairList.Count, calculatedPairList.Count);
            CollectionAssert.AreEqual(expectedPairList, testCalculator.GetPairsFor(10));
        }

        [Test]
        public void GetPairs_All_ReturnsListContainingAllValidPairs()
        {
            var expectedPairList = new List<Tuple<int, int>> { new Tuple<int, int>(3, 7), new Tuple<int, int>(5, 5) };

            var testCalculator = new GoldbachPairCalculator(TestCache);
            var calculatedPairList = testCalculator.GetPairsFor(10, true).ToList();

            Assert.AreEqual(expectedPairList.Count, calculatedPairList.Count);
            CollectionAssert.AreEqual(expectedPairList, calculatedPairList);
        }
    }
}
