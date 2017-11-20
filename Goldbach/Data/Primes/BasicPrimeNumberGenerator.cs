using System;
using System.Collections.Generic;
using System.Linq;

namespace Goldbach.Data.Primes
{
    /// <summary>
    /// Prime number generator, which finds primes by ensuring that they have no exact divisors.
    /// </summary>
    public class BasicPrimeNumberGenerator : PrimeNumberGeneratorBase
    {
        public override IEnumerable<int> GeneratePrimesBetween(int start, int end)
        {
            //Check its a valid range to generate primes across
            ValidateRange(start, end);


            var primes = new List<int>();

            //Do we need to add two to the output list?
            if (start <= 2)
            {
                primes.Add(2);
            }

            //Check we'll start on an odd number
            if(start % 2 == 0)
            {
                start++;
            }

            for (var p = start; p <= end; p += 2)
            {
                //Get all possible factors for the current test prime. Check if it does not divide evenly by any of them
                //This may be getting more factors than is strictly necessary
                var checkFactors = Enumerable.Range(3, (int)Math.Sqrt(p) - 1);
                if (!checkFactors.Any(f => p % f == 0))
                {
                    primes.Add(p);
                }
            }

            //Return primes
            return primes;
        }
    }
}
