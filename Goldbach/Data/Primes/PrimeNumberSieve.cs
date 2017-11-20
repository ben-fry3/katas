using System;
using System.Collections.Generic;
using System.Linq;

namespace Goldbach.Data.Primes
{
    /// <summary>
    /// Provides a seiving based search for prime numbers
    /// </summary>
    class PrimeNumberSieve : PrimeNumberGeneratorBase
    {
        public override IEnumerable<int> GeneratePrimesBetween(int start, int end)
        {
            //Check its a valid range to generate primes across
            ValidateRange(start, end);

            //Generate a list of all the numbers in the range, then remove all even ones, except two
            var primes = Enumerable.Range(start, end - (start - 1)).Where(x => x == 2 || x % 2 != 0);

            for(var i = 3; i < Math.Sqrt(end); i += 2)
            {
                //Avoid any potential issues with the loop variable and linq closure
                var factor = i;
                primes = primes.Where(x => x == factor || x % factor != 0);
            }

            return primes;
        }
    }
}
