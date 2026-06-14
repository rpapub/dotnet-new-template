using System;
using System.Collections.Generic;
using Cpmf.CodedActivities;
using Cpmf.CodedActivities.Exceptions;
using Xunit;

namespace Cpmf.CodedActivities.Tests
{
    public class ErrorClassifierTests
    {
        public static IEnumerable<object[]> Cases => new List<object[]>
        {
            new object[] { new Exceptions.TimeoutException("t"),  true  },
            new object[] { new Exceptions.NetworkException("t"),  true  },
            new object[] { new AuthException("t"),         false },
            new object[] { new HostKeyException("t"),      false },
            new object[] { new PermissionException("t"),   false },
            new object[] { new PathNotFoundException("t"), false },
            new object[] { new ArgumentException("t"),     false },
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
