using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Goldbach.Data.Primes
{
    /// <summary>
    /// Provides prime numbers based on reading from a file on disk.
    /// The file in question was obtained from <see href="http://www.naturalnumbers.org/primes.html">naturalnumbers.org</see>
    /// File structure is comma separated, with one line per prime
    /// Each line is formed of the sequential rank, the prime itself, and the interval from its predecessor
    /// </summary>
    public class PrimeNumberReader : PrimeNumberGeneratorBase
    {
        public override IEnumerable<int> GeneratePrimesBetween(int start, int end)
        {
            //Check its a valid range to generate primes across
            ValidateRange(start, end);

            var primes = new List<int>();

            //Get the resource file, and open a stream to it
            var assembly = Assembly.GetExecutingAssembly();
            var primesResourceName = "Goldbach.Data.Resources.primes.txt";

            using (var stream = assembly.GetManifestResourceStream(primesResourceName))
            using (var primeReader = new StreamReader(stream))
            {
                var castPrime = 0;
                while (!primeReader.EndOfStream && castPrime <= end)
                {
                    //Get the line, and split on commas
                    var line = primeReader.ReadLine();
                    var parts = line.Split(',');

                    //Pick the second item from the split, and cast it to an integer
                    if (int.TryParse(parts[1], out castPrime) && (castPrime >= start && castPrime <= end))
                    {
                        primes.Add(castPrime);
                    }
                }
            }

            return primes;
        }
    }
}