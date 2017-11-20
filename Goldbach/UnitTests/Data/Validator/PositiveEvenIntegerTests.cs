using Goldbach.Data.Validator;
using NUnit.Framework;
using System;

namespace Goldbach.Tests.Validator
{
    [TestFixture]
    public class PositiveEvenIntegerTests
    {
        internal PositiveEvenIntegerValidator TestValidator { get { return new PositiveEvenIntegerValidator(); } }

        [Test]
        public void ValidateAndConvertInput_DoesNotCastFromString_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TestValidator.ValidateAndConvert("foo"));
        }

        [TestCase(0)]
        [TestCase(-2)]
        [TestCase(1)]
        public void ValidateInput_InvalidCaseThatDoedCast_ReturnsFalse(int testValue)
        {
            Assert.That(TestValidator.IsValid(testValue), Is.Not.True);
        }

        [TestCase(2)]
        [TestCase(4)]
        public void ValidateInput_ValidCase_ReturnsTrue(int testValue)
        {
            Assert.That(TestValidator.IsValid(testValue), Is.True);
        }

        [Test]
        public void ValidateAndConvertInput_CastsFromStringAndIsValid_ReturnsConvertedValue()
        {
            var testString = "6";
            var expectedValue = 6;

            Assert.That(TestValidator.ValidateAndConvert(testString), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ValidateAndConvertInput_CastsFromStringAndIsNotValid_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TestValidator.ValidateAndConvert("5"));
        }
    }
}
