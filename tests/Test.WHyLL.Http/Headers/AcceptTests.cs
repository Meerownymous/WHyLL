﻿using WHyLL.Http.Headers;
using Xunit;

namespace Test.WHyLL.Http.Headers
{
	public sealed class AcceptTests
	{
        [Fact]
        public void HasHeaderName()
        {
            Assert.Equal(
                "Accept",
                new Accept("application/foobar").Key()
            );
        }

        [Fact]
		public void IncludesContentType()
		{
			Assert.Equal(
                "application/json",
                new Accept("application/json").Value()
			);
        }
	}
}
