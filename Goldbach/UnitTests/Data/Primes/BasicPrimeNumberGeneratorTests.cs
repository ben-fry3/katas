using Goldbach.Data.Primes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Goldbach.Tests.Primes
{
    [TestFixture]
    public class BasicPrimeNumberGeneratorTests
    {
        private BasicPrimeNumberGenerator BasicGenerator => new BasicPrimeNumberGenerator();

        //Check that the range is being validated
        [Test]
        public void BasicGenerator_GenerateBetween_InvalidStart_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => BasicGenerator.GeneratePrimesBetween(-1, 10));
        }

        [Test]
        public void BasicGenerator_GenerateBetween_InvalidRange_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => BasicGenerator.GeneratePrimesBetween(10, 5));
        }

        [Test]
        public void BasicGenerator_GenerateTo_InvalidEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => BasicGenerator.GeneratePrimesTo(1));
        }

        //And that it's generating primes as expected
        [Test]
        public void BasicGenerator_GenerateTo_ValidEnd_ReturnsExpectedPrimes()
        {
            var expectedPrimes = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };

            var generatedPrimes = BasicGenerator.GeneratePrimesTo(20).ToList();

            CollectionAssert.AreEqual(expectedPrimes, generatedPrimes);
        }

        [Test]
        public void BasicGenerator_GenerateBetween_ValidRange_ReturnsExpectedPRimes()
        {
            var expectrdPrimes = new List<int> { 11, 13, 17, 19, 23, 29 };

            var generatedPrimes = BasicGenerator.GeneratePrimesBetween(10, 30);

            CollectionAssert.AreEqual(expectrdPrimes, generatedPrimes);
        }

        //Few timed generations, for comparison with other methods
        [TestCase(100, 25, Explicit = true)]
        [TestCase(1000, 168, Explicit = true)]
        [TestCase(10000, 1229, Explicit = true)]
        [TestCase(100000, 9592, Explicit = true)]
        [TestCase(1000000, 78498, Explicit = true)]
        [TestCase(10000000, 664579, Explicit = true)]
        [TestCase(15485863, 1000000, Explicit = true)] //First million primes - matches the available test for the reader
        [TestCase(100000000, 5761455, Explicit = true)]
        public void BasicGenerator_GeneratePrimesTo_ReturnsExpectedCount_AndTimesForFun(int limit, int expectedCount)
        {
            var timer = Stopwatch.StartNew();

            var generatedPrimes = BasicGenerator.GeneratePrimesTo(limit).ToList();

            timer.Stop();

            Assert.AreEqual(expectedCount, generatedPrimes.Count());
            Console.WriteLine($"Generated all {expectedCount} primes below {limit} in {timer.ElapsedMilliseconds} ms");
        }
    }
}
