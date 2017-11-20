using System.Collections.Generic;

namespace Goldbach.Data.Primes
{
    public interface IPrimeNumberCache
    {
        IList<int> GetPrimesTo(int target);
    }
}
