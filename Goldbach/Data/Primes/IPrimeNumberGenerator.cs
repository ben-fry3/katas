using System;
using System.Collections.Generic;
using System.Linq;

namespace Goldbach.Data.Primes
{

    public interface IPrimeNumberGenerator
    {
        IEnumerable<int> GeneratePrimesBetween(int start, int end);

        IEnumerable<int> GeneratePrimesTo(int end);
    }
}
