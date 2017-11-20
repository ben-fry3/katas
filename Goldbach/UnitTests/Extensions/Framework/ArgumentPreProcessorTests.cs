using Goldbach.Data.Extensions.Framework;
using NUnit.Framework;

namespace Goldbach.UnitTests.Extensions.Framework
{
    [TestFixture]
    public class ArgumentPreProcessorTests
    {
        [TestCase("/foo", "foo")]
        [TestCase("-bar", "bar")]
        public void Parse_ArgumentsWithValidPrefix_ReturnsReducedValue(string preProcess, string expected)
        {
            var processed = preProcess.ParseArgument();

            Assert.AreEqual(expected, processed);
        }

        [Test]
        public void Parse_ArgumentWithNoPrefix_ReturnsNull()
        {
            var processed = "baz".ParseArgument();

            Assert.That(processed, Is.Null);
        }

        [Test]
        public void Parse_ArgumentWithValidPrefix_ContainsInvalidCharacter_ReturnsNull()
        {
            var preProcessed = "-?foo";
            var processed = preProcessed.ParseArgument();

            Assert.That(processed, Is.Null);
        }

        [Test]
        public void Parse_ArgumentWithNoPrefix_ContainsInvalidCharacter_ReturnsNull()
        {
            var preProcessed = "foo!";
            var processed = preProcessed.ParseArgument();

            Assert.That(processed, Is.Null);
        }

        [Test]
        public void ParseAll_ArgumentsWithValidPrefixes_ReturnsReducedValues()
        {
            var expectedArguments = new string[] { "foo", "bar" };

            var preProcessedArguments = new string[] { "-foo", "/bar" };
            var processedArguments = preProcessedArguments.ParseAllArguments();

            CollectionAssert.AreEqual(expectedArguments, processedArguments);
        }

        [Test]
        public void ParseAll_WithInvalidArguments_ReturnsNulls()
        {
            var preProcessesArguments = new string[] { "?a", "-?all", "/foo!", "--bar" };
            var processedArguments = preProcessesArguments.ParseAllArguments();

            Assert.AreEqual(preProcessesArguments.Length, processedArguments.Length);
            
            foreach(var processedArg in processedArguments)
            {
                Assert.That(processedArg, Is.Null);
            }
        }

        [Test]
        public void CheckFor_ListContainsArgument_ReturnsTrue()
        {
            var checkArguments = new string[] { "a", "all" };
            var suppliedArguments = new string[] { "-foo", "/all" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.True);
        }

        [Test]
        public void CheckFor_ListDoesNotContainArgument_ReturnsFalse()
        {
            var checkArguments = new string[] { "all" };
            var suppliedArguments = new string[] { "-foo", "-bar", "-baz" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.Not.True);
        }

        [Test]
        public void CheckFor_ListContainsArgumentAndInvalid_ReturnsTrue()
        {
            var checkArguments = new string[] { "bar" };
            var suppliedArguments = new string[] { "foo!", "/bar" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.True);
        }

        [Test]
        public void CheckFor_ListDoesNotContainArgumentAndInvalid_ReturnsFalse()
        {
            var checkArguments = new string[] { "foo" };
            var suppliedArguments = new string[] { "foo!", "-bar" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.Not.True);
        }

        [Test]
        public void CheckFor_SingleMatchingArgument_ReturnsTrue()
        {
            var checkArguments = new string[] { "a", "all" };
            var suppliedArguments = new string[] { "-all" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.True);
        }

        [Test]
        public void TestFor_IncompatibleCasing_ReturnsFalse()
        {
            var checkArguments = new string[] { "all" };
            var suppliedArguments = new string[] { "/ALL" };

            Assert.That(suppliedArguments.CheckFor(checkArguments), Is.Not.True);
        }
    }
}
