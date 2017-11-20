using System.Collections.Generic;
using System.Linq;

namespace Goldbach.Data.Primes
{
    /// <summary>
    /// Wraps a prime number generator, and some in memory storage
    /// Should allow for only generating a given prime only once, per given program run - even
    /// if the possibility of generating Goldbach pairs for multiple inputs in one go were added
    /// </summary>
    public class PrimeNumberCache : IPrimeNumberCache
    {
        private IPrimeNumberGenerator _generator;

        private List<int> Primes { get; set; }

        /// <summary>
        /// Either retrieves from memory, generates, or a combination of both, all primes upto
        /// a given value
        /// </summary>
        /// <param name="target">The maximum limit on the value of primes</param>
        /// <returns>A list of all primes less than or equal to the provided target</returns>
        public IList<int> GetPrimesTo(int target)
        {
            //If there's no stored primes, generate them all
            if (!Primes.Any())
            {
                Primes = _generator.GeneratePrimesTo(target).ToList();
            }
            else
            {
                //Is the last generated prime greater than the target
                var lastPrime = Primes.Last();
                if (lastPrime > target)
                {
                    //Just return the primes upto that point
                    return Primes.Where(p => p <= target).ToList();
                }

                //Add the primes between the current maximum and the target
                Primes.AddRange(_generator.GeneratePrimesBetween(lastPrime + 1, target));
            }
            return Primes.ToList();
        }

        /// <summary>
        /// Constructor which will initialize the internal store of prime numbers, and either create a
        /// default <see cref="IPrimeNumberGenerator"/>, or store the one provided
        /// </summary>
        /// <param name="requestedGenerator">An optional prime number generator. If not specified
        /// <see cref="BasicPrimeNumberGenerator"/> is used as a default</param>
        public PrimeNumberCache(IPrimeNumberGenerator requestedGenerator = null)
        {
            Primes = new List<int>();

            _generator = requestedGenerator ?? new BasicPrimeNumberGenerator();
        }

        /// <summary>
        /// Constructor for testing purposes, which will allow the initial state of the internal primes
        /// list be set, as well as providing options to set the generator
        /// </summary>
        /// <param name="initialPrimes">List of integers to initialize the primes to</param>
        /// <param name="requestedGenerator">Optional prime number generator - treated the same as in the default constructor</param>
        internal PrimeNumberCache(IList<int> initialPrimes, IPrimeNumberGenerator requestedGenerator = null) : this(requestedGenerator)
        {
            Primes = (List<int>)initialPrimes;
        }
    }
}
