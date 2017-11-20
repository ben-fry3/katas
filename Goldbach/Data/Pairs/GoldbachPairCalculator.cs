using Goldbach.Data.Primes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Pairs
{
    /// <summary>
    /// Class wrapping everything that is required to generate Goldbach pairs for a given number
    /// </summary>
    public class GoldbachPairCalculator
    {
        private IPrimeNumberCache _cache;

        /// <summary>
        /// Method to retrieve Goldbach pairs for a given integer
        /// </summary>
        /// <param name="number">The number that the pairs must sum to</param>
        /// <param name="returnAll">Flag, representing if only one, or all possible distinct pairs should be returned</param>
        /// <returns>An enumerable of pair(s) of primes which sum to the provided number</returns>
        public IEnumerable<Tuple<int, int>> GetPairsFor(int number, bool returnAll = false)
        {
            foreach (var prime in _cache.GetPrimesTo(number / 2).Where(p => p >= 3))
            {
                var testNumber = number - prime;

                if (_cache.GetPrimesTo(number).Contains(testNumber))
                {
                    yield return new Tuple<int, int>(prime, testNumber);

                    if (!returnAll)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Default constructor, which will initialise the contained <see cref="PrimeNumberCache"/> with a default prime number generator
        /// </summary>
        public GoldbachPairCalculator()
        {
            _cache = new PrimeNumberCache();
        }

        /// <summary>
        /// Optional constructor, allowing the genrator of the contained <see cref="PrimeNumberCache"/> to be set
        /// </summary>
        /// <param name="requestedGenerator">The prime number generator to request the cache to use</param>
        public GoldbachPairCalculator(IPrimeNumberGenerator requestedGenerator)
        {
            _cache = new PrimeNumberCache(requestedGenerator);
        }

        /// <summary>
        /// Constructor for testinf purposes, allowing a specific - maybe mock - prime number cache to be used
        /// </summary>
        /// <param name="cache"></param>
        internal GoldbachPairCalculator(IPrimeNumberCache cache)
        {
            _cache = cache;
        }
    }
}
