using System;
using Whyre.Parts;
using Xunit;

namespace WHYRE.Parts.Test
{
	public sealed class RequestLineTests
	{
		[Theory]
		[InlineData("GET")]
        [InlineData("PUT")]
        [InlineData("POST")]
        [InlineData("DELETE")]
        [InlineData("CONNECT")]
        [InlineData("OPTIONS")]
        [InlineData("HEAD")]
        [InlineData("TRACE")]
        public void MakesRelativeResourceLine(string method)
		{
			Assert.Equal(
				$"{method} /slash/slash/boom HTTP/1.1\r\n",
				new RequestLine(
					"GET",
					new Uri("/slash/slash/boom"),
					new Version(1, 1)
				).Value()
			);
		}

        [Fact]
        public void RejectsUnknownMethod()
        {
            Assert.Throws<ArgumentException>(() =>
                new RequestLine(
                    "DESTROY",
                    new Uri("/slash/slash/boom"),
                    new Version(1, 1)
                ).Value()
            );
        }

        [Fact]
        public void RejectsUnspecifiedResourceWhenNotOptionsRequest()
        {
            Assert.Throws<ArgumentException>(() =>
                new RequestLine(
                    "GET",
                    new Version(1, 1)
                ).Value()
            );
        }

        [Fact]
        public void AllowsUnspecifiedResourceWhenOptionsRequest()
        {
            Assert.Equal(
                "OPTIONS * HTTP/1.1\r\n",
                new RequestLine(
                    "OPTIONS",
                    new Version(1, 1)
                ).Value()
            );
        }
    }
}

