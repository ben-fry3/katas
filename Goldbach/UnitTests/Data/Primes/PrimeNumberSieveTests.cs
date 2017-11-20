using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Goldbach.Data.Primes;
using NUnit.Framework;

namespace Goldbach.UnitTests.Data.Primes
{
    [TestFixture]
    public class PrimeNumberSieveTests
    {
        private PrimeNumberSieve PrimeSieve => new PrimeNumberSieve();

        //Ensure validation occurrs
        [Test]
        public void PrimeSieve_GenerateBetween_InvalidStart_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeSieve.GeneratePrimesBetween(-1, 10));
        }

        [Test]
        public void PrimeSieve_GenerateBetween_InvalidRange_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeSieve.GeneratePrimesBetween(10, 5));
        }

        [Test]
        public void PrimeSieve_GenerateTo_InvalidEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => PrimeSieve.GeneratePrimesTo(1));
        }

        //Check the filtered numbers are primes
        [Test]
        public void PrimeSieve_GenerateTo_ValidEnd_ReturnPrimes()
        {
            var expectedPrimes = new List<int> { 2, 3, 5, 7 };

            var generatedPrimes = PrimeSieve.GeneratePrimesTo(10);

            CollectionAssert.AreEqual(expectedPrimes, generatedPrimes);
        }

        [Test]
        public void PrimeSieve_GenerateBetween_ValidRange_ReturnsExpectedPrimes()
        {
            var expectedPrimes = new List<int> { 17, 19, 23, 29 };

            var generatedPrimes = PrimeSieve.GeneratePrimesBetween(15, 30);

            CollectionAssert.AreEqual(expectedPrimes, generatedPrimes);
        }

        //Timed tests for comparisons
        [TestCase(100, 25, Explicit = true)]
        [TestCase(1000, 168, Explicit = true)]
        [TestCase(10000, 1229, Explicit = true)]
        [TestCase(100000, 9592, Explicit = true)]
        [TestCase(1000000, 78498, Explicit = true)]
        [TestCase(10000000, 664579, Explicit = true)]
        [TestCase(15485863, 1000000, Explicit = true)] //First million primes - matches the available test for the reader
        public void PrimeSieve_GeneratePrimesTo_ReturnsExpectedCount_AndTimesForFun(int limit, int expectedCount)
        {
            var timer = Stopwatch.StartNew();

            var generatedPrimes = PrimeSieve.GeneratePrimesTo(limit).ToList();

            timer.Stop();

            Assert.AreEqual(expectedCount, generatedPrimes.Count());
            Console.WriteLine($"Generated all {expectedCount} primes below {limit} in {timer.ElapsedMilliseconds} ms");
        }
    }
}
