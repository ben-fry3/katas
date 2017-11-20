using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Mines.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        private void BreakToStringAssertion(IList<string> expectedStrings, IList<string> testStrings)
        {
            Assert.That(testStrings.Count, Is.EqualTo(expectedStrings.Count));
            CollectionAssert.AreEqual(expectedStrings, testStrings);
        }

        private void CombineAssertion(string expectedString, string testString)
        {
            Assert.That(testString, Is.EqualTo(expectedString));
        }

        [Test]
        public void StringBreakToLenght_IntegerMultipleOfLength_ReturnsExpectedStrings()
        {
            var expectedStrings = new List<string>
            {
                "0123", "4567", "89ab", "cdef"
            };

            var brokenStrings = "0123456789abcdef".BreakToLength(4).ToList();

            BreakToStringAssertion(expectedStrings, brokenStrings);
        }

        [Test]
        public void StringBreakToLength_NotIntegerMultipleOfLength_ReturnsPaddedStrings()
        {
            var expectedStrings = new List<string>
            {
                "0123", "4567", "89  "
            };

            var brokenStrings = "0123456789".BreakToLength(4).ToList();

            BreakToStringAssertion(expectedStrings, brokenStrings);
        }

        [Test]
        public void StringBreakToLength_IsExactlyLength_ReturnsOneString()
        {
            var expectedStrings = new List<string>
            {
                "0123456"
            };

            var brokenStrings = "0123456".BreakToLength(7).ToList();

            BreakToStringAssertion(expectedStrings, brokenStrings);
        }

        [Test]
        public void StringBreakToLength_LessThanLength_ReturnsOnePaddedString()
        {
            var expectedStrings = new List<string>
            {
                "foo  "
            };

            var brokenStrings = "foo".BreakToLength(5).ToList();

            BreakToStringAssertion(expectedStrings, brokenStrings);
        }

        [Test]
        public void StringBreakToLength_EmptyString_ReturnsEmptyEnumerable()
        {
            var expectedStrings = new List<string>();
            var brokenStrings = string.Empty.BreakToLength(1).ToList();

            BreakToStringAssertion(expectedStrings, brokenStrings);
        }

        [Test]
        public void CombineStrings_ListOfStrings_ReturnsSingleString()
        {
            var expectedString = "foobar";
            var combinedString = new List<string> { "foo", "bar" }.Combine();

            CombineAssertion(expectedString, combinedString);
        }

        [Test]
        public void CombineStrings_OnlyOneInList_ReturnsSingleString()
        {
            var expectedString = "baz";
            var combinedString = new List<string> { "baz" }.Combine();

            CombineAssertion(expectedString, combinedString);
        }

        [Test]
        public void CombineStrings_EmptySet_ReturnsEmptyString()
        {
            var expectedString = string.Empty;
            var combinedString = new List<string>().Combine();

            CombineAssertion(expectedString, combinedString);
        }
    }
}
