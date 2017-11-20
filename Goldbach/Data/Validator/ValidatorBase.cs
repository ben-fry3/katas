using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Goldbach.Data.Validator
{
    /// <summary>
    /// Basic validator class, which provides skeleton implementations of casting from string
    /// to the target type, and iterating through the configured ruels to check for validity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValidatorBase<T> : IValidator<T>
    {
        public virtual IList<Expression<Func<T, bool>>> Rules { get; }

        /// <summary>
        /// Take a cast value, and ensure that it meets the implemented validation constraints
        /// </summary>
        /// <param name="value">The value to check against the rules</param>
        /// <returns>Boolean, representing if the supplied value met the rules</returns>
        public bool IsValid(T value)
        {
            var isValid = true;

            foreach (var rule in Rules)
            {
                var compiledRule = rule.Compile();
                isValid = compiledRule(value) && isValid;
            }

            return isValid;
        }

        /// <summary>
        /// Attempt to convert a string to the target type, then execute the validation rules
        /// against that converted value
        /// </summary>
        /// <param name="value">String representation of the value to convert and validate</param>
        /// <returns>The converted value, if it met the prescribed validation constraints</returns>
        public T ValidateAndConvert(string value)
        {
            T convertedValue;

            try
            {
                convertedValue = (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                //Bit more specific exception message - set inner exception for debugging use
                throw new ArgumentException($"Value “{value}” cannot be cast to specified type", nameof(value), ex);
            }

            if(!IsValid(convertedValue))
            {
                throw new ArgumentException($"Supplied value “{value}” does not meet the specified validation rules", nameof(value));
            }

            return convertedValue;
        }
    }
}
