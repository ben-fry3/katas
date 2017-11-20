using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Goldbach.Data.Primes;
using NUnit.Framework;

namespace Goldbach.Tests.Primes
{
    [TestFixture]
    public class PrimeNumberReaderTests
    {
        private PrimeNumberReader PrimeReader => new PrimeNumberReader();

        //Check that validation is being invoked
        [Test]
        public void PrimeReader_GenerateBetween_InvalidStart_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeReader.GeneratePrimesBetween(-1, 10));
        }

        [Test]
        public void PrimeReader_GenerateBetween_InvalidRange_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeReader.GeneratePrimesBetween(10, 5));
        }

        [Test]
        public void PrimeReader_GenerateTo_InvalidEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeReader.GeneratePrimesTo(1));
        }

        //Ensure expected primes are fetched
        [Test]
        public void PrimeReader_GenerateTo_ValidEnd_ReturnsExpectedPrimes()
        {
            var expectedPrimes = new List<int> {2, 3, 5, 7};

            var generatedPrimes = PrimeReader.GeneratePrimesTo(10);

            CollectionAssert.AreEqual(expectedPrimes, generatedPrimes);
        }

        [Test]
        public void PrimeReader_GenerateBetween_ValidRange_ReturnsExpectedPrimes()
        {
            var expectedPrimes = new List<int> {11, 13, 17, 19, 23, 29};

            var generatedPrimes = PrimeReader.GeneratePrimesBetween(11, 30);

            CollectionAssert.AreEqual(expectedPrimes, generatedPrimes);
        }

        //Timed tests for comparison
        [TestCase(100, 25, Explicit = true)]
        [TestCase(1000, 168, Explicit = true)]
        [TestCase(10000, 1229, Explicit = true)]
        [TestCase(100000, 9592, Explicit = true)]
        [TestCase(1000000, 78498, Explicit = true)]
        [TestCase(10000000, 664579, Explicit = true)]
        [TestCase(15485863, 1000000, Explicit = true)] //Only have the first million primes available
        public void PrimeReader_GeneratePrimesTo_ReturnsExpectedCount_AndTimesForFun(int limit, int expectedCount)
        {
            var timer = Stopwatch.StartNew();

            var generatedPrimes = PrimeReader.GeneratePrimesTo(limit).ToList();

            timer.Stop();

            Assert.AreEqual(expectedCount, generatedPrimes.Count());
            Console.WriteLine($"Generated all {expectedCount} primes below {limit} in {timer.ElapsedMilliseconds} ms");
        }
    }
}