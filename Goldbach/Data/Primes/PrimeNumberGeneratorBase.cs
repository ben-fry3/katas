using System;
using System.Collections.Generic;

namespace Goldbach.Data.Primes
{
    /// <summary>
    /// Base class for prime number generators, providing an integer range validation, and a base implementation of
    /// <see cref="GeneratePrimesTo(int)"/>, simply invoking <see cref="GeneratePrimesBetween(int, int)"/> with 2 - the smallest prime
    /// as its start
    /// </summary>
    public abstract class PrimeNumberGeneratorBase : IPrimeNumberGenerator
    {
        internal void ValidateRange(int start, int end)
        {
            if (start < 0)
            {
                throw new ArgumentException("Cannot start generating primes from a negative number", nameof(start));
            }
            if (end < start)
            {
                throw new ArgumentException("Prime number generation space cannot finish lower than it starts", nameof(end));
            }
        }

        public virtual IEnumerable<int> GeneratePrimesBetween(int start, int end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GeneratePrimesTo(int end)
        {
            return GeneratePrimesBetween(2, end);
        }
    }
}
