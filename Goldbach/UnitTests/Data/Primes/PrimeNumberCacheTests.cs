using FakeItEasy;
using Goldbach.Data.Primes;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Data.Primes
{
    [TestFixture]
    class PrimeNumberCacheTests
    {
        private IPrimeNumberGenerator TestGenerator
        {
            get
            {
                var testGenerator = A.Fake<IPrimeNumberGenerator>();
                A.CallTo(() => testGenerator.GeneratePrimesTo(A<int>._)).Returns(new List<int>());
                A.CallTo(() => testGenerator.GeneratePrimesBetween(A<int>._, A<int>._)).Returns(new List<int>());

                return testGenerator;
            }
        }

        [Test]
        public void GetPrimes_NoExistingList_CallsGenerateTo()
        {
            var localTestGenerator = TestGenerator;

            var testCache = new PrimeNumberCache(localTestGenerator);

            testCache.GetPrimesTo(10);

            A.CallTo(() => localTestGenerator.GeneratePrimesTo(A<int>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void GetPrimes_ExistingList_TargetGreater_CallsGenerateBetween()
        {
            var primes = new List<int> { 2, 3, 5 };
            var localTestGenerator = TestGenerator;

            var testCache = new PrimeNumberCache(primes, localTestGenerator);

            testCache.GetPrimesTo(10);

            A.CallTo(() => localTestGenerator.GeneratePrimesBetween(A<int>._, A<int>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => localTestGenerator.GeneratePrimesTo(A<int>._)).MustNotHaveHappened();
        }

        [Test]
        public void GetPrimes_ExisitngList_TargetLess_DoesNotCallGenerator()
        {
            var initialPrimes = new List<int> { 2, 3, 5, 7, 11 };
            var localTestGenerator = TestGenerator;

            var testCache = new PrimeNumberCache(initialPrimes, localTestGenerator);

            testCache.GetPrimesTo(5);

            A.CallTo(() => localTestGenerator.GeneratePrimesBetween(A<int>._, A<int>._)).MustNotHaveHappened();
            A.CallTo(() => localTestGenerator.GeneratePrimesTo(A<int>._)).MustNotHaveHappened();
        }

        [Test]
        public void GetPrimes_NoExistingList_ReturnsExpectedPrimes()
        {
            var expectedPrimes = new List<int> { 2, 3, 5, 7 };
            var cachedPrimes = new PrimeNumberCache().GetPrimesTo(10);

            CollectionAssert.AreEqual(expectedPrimes, cachedPrimes);
        }

        [Test]
        public void GetPrimes_ExistingList_TargetGreater_ReturnsExpectedPrimes()
        {
            var initialPrimes = new List<int> { 2, 3, 5, 7 };
            var expectedPrimes = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };
            var cachedPrimes = new PrimeNumberCache(initialPrimes).GetPrimesTo(20);

            CollectionAssert.AreEqual(expectedPrimes, cachedPrimes);
        }

        [Test]
        public void GetPrimes_ExistingList_TargetLess_ReturnsExpectedPrimes()
        {
            var initialPrimes = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };
            var expectedPrimes = new List<int> { 2, 3, 5, 7 };
            var cachedPrimes = new PrimeNumberCache(initialPrimes).GetPrimesTo(10);

            CollectionAssert.AreEqual(expectedPrimes, cachedPrimes);
        }
    }
}
