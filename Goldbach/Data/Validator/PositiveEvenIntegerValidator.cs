using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Goldbach.Data.Validator
{
    /// <summary>
    /// Specific validator implementation, to check that a value is a positive integer
    /// </summary>
    public class PositiveEvenIntegerValidator : ValidatorBase<int>
    {
        public override IList<Expression<Func<int, bool>>> Rules { get; }

        /// <summary>
        /// Constructor, that will initialise the <see cref="Rules"/> property to the following:
        /// Value must be greater than zero
        /// Value must be exactly divisible by two
        /// </summary>
        public PositiveEvenIntegerValidator()
        {
            Rules = new List<Expression<Func<int, bool>>>
            {
                x => x > 0,
                x => x % 2 == 0
            };
        }
    }
}
