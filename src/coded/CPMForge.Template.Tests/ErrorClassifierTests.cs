using System;
using System.Collections.Generic;
using CPMForge.Template;
using CPMForge.Template.Exceptions;
using Xunit;

namespace CPMForge.Template.Tests
{
    public class ErrorClassifierTests
    {
        public static IEnumerable<object[]> Cases => new List<object[]>
        {
            new object[] { new VaultTimeoutException("t"),  true  },
            new object[] { new VaultAuthException("t"),     false },
            new object[] { new VaultNotFoundException("t"), false },
            new object[] { new ArgumentException("t"),         false },
            new object[] { new InvalidOperationException("t"), false },
        };

        [Theory]
        [MemberData(nameof(Cases))]
        public void IsRetryable_ReturnsExpected(Exception ex, bool expected)
        {
            Assert.Equal(expected, ErrorClassifier.IsRetryable(ex));
        }
    }
}
