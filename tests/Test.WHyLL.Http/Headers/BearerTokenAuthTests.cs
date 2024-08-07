﻿using WHyLL.Http.Headers;
using Xunit;

namespace Test.WHyLL.Http.Headers
{
	public sealed class BearerTokenAuthTests
	{
        [Fact]
        public void HasHeaderName()
        {
            Assert.Equal(
                "Authorization",
                new BearerTokenAuth("34uuk22p3j23jre233ij").Key()
            );
        }

        [Fact]
		public void IncludesToken()
		{
			Assert.Equal(
                "Bearer 34uuk22p3j23jre233ij",
                new BearerTokenAuth("34uuk22p3j23jre233ij").Value()
			);
        }
	}
}

