using Data.Pairs;
using Goldbach.Data.Extensions.Framework;
using Goldbach.Data.Validator;
using System;
using System.Linq;

namespace Goldbach
{
    public class Program
    {
        static string[] AllPairArguments = { "a", "all" };

        private static IValidator<int> InputValidator = new PositiveEvenIntegerValidator();

        static void Main(string[] args)
        {
            string inputNumber;
            int castNumber;

            if(!args.Any())
            {
                Console.Write("No number specidied in arguments. Please enter a number: ");
                inputNumber = Console.ReadLine();
            }
            else
            {
                inputNumber = args.First();
            }

            try
            {
                castNumber = InputValidator.ValidateAndConvert(inputNumber);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Validation or conversion failed. Exception message was:\n{ex.Message}");
                Console.ReadLine();
                return;
            }

            var calculator = new GoldbachPairCalculator();

            Console.WriteLine($"Goldbach pairs for {castNumber}:");
            foreach(var pair in calculator.GetPairsFor(castNumber, args.CheckFor(AllPairArguments)))
            {
                Console.WriteLine($"{pair.Item1} & {pair.Item2}");
            }

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
