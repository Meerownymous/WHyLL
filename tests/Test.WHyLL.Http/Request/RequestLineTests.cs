using Xunit;

namespace WHyLL.Http.Request.Test
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
					method,
					new Uri("/slash/slash/boom", uriKind: UriKind.Relative),
					new Version(1, 1)
				).AsString()
			);
		}

        [Theory]
        [InlineData("GET")]
        [InlineData("PUT")]
        [InlineData("POST")]
        [InlineData("DELETE")]
        [InlineData("CONNECT")]
        [InlineData("OPTIONS")]
        [InlineData("HEAD")]
        [InlineData("TRACE")]
        public void MakesAbsoluteResourceLine(string method)
        {
            Assert.Equal(
                $"{method} http://www.enhanced-calm.com/slash/slash/boom HTTP/1.1\r\n",
                new RequestLine(
                    method,
                    new Uri("http://www.enhanced-calm.com/slash/slash/boom"),
                    new Version(1, 1)
                ).AsString()
            );
        }

        [Fact]
        public void AddsQuery()
        {
            Assert.Equal(
                $"GET http://www.enhanced-calm.com/resource?slash=boom HTTP/1.1\r\n",
                new RequestLine(
                    "GET",
                    new Uri("http://www.enhanced-calm.com/resource?slash=boom"),
                    new Version(1, 1)
                ).AsString()
            );
        }

        [Fact]
        public void AddsPort()
        {
            Assert.Equal(
                $"GET http://www.enhanced-calm.com:1337/resource?slash=boom HTTP/1.1\r\n",
                new RequestLine(
                    "GET",
                    new Uri("http://www.enhanced-calm.com:1337/resource?slash=boom"),
                    new Version(1, 1)
                ).AsString()
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
                ).AsString()
            );
        }

        [Fact]
        public void RejectsUnspecifiedResourceWhenNotOptionsRequest()
        {
            Assert.Throws<ArgumentException>(() =>
                new RequestLine(
                    "GET",
                    new Version(1, 1)
                ).AsString()
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
                ).AsString()
            );
        }
    }
}

