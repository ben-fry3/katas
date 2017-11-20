using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Goldbach.Data.Validator
{
    public interface IValidator<T>
    {
        IList<Expression<Func<T, bool>>> Rules { get; }

        bool IsValid(T value);

        T ValidateAndConvert(string value);
    }
}
