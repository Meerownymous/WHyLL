using WHyLL.Http.Request;
using Xunit;

namespace Test.WHyLL.Http.Request
{
	public sealed class RequestPrologueTests
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
				$"{method} /slash/slash/boom HTTP/1.1",
                string.Join(" ",
				    new RequestPrologue(
					    method,
					    "/slash/slash/boom",
					    new Version(1, 1)
				    ).Sequence()
                )
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
                $"{method} http://www.enhanced-calm.com/slash/slash/boom HTTP/1.1",
                string.Join(" ",
                    new RequestPrologue(
                        method,
                        "http://www.enhanced-calm.com/slash/slash/boom",
                        new Version(1, 1)
                    ).Sequence()
                )
            );
        }

        [Fact]
        public void AddsQuery()
        {
            Assert.Equal(
                $"GET http://www.enhanced-calm.com/resource?slash=boom HTTP/1.1",
                string.Join(" ",
                    new RequestPrologue(
                        "GET",
                        "http://www.enhanced-calm.com/resource?slash=boom",
                        new Version(1, 1)
                    ).Sequence()
                )
            );
        }

        [Fact]
        public void AddsPort()
        {
            Assert.Equal(
                $"GET http://www.enhanced-calm.com:1337/resource?slash=boom HTTP/1.1",
                string.Join(" ",
                    new RequestPrologue(
                        "GET",
                        "http://www.enhanced-calm.com:1337/resource?slash=boom",
                        new Version(1, 1)
                    ).Sequence()
                )
            );
        }

        [Fact]
        public void RejectsUnknownMethod()
        {
            Assert.Throws<ArgumentException>(() =>
                new RequestPrologue(
                    "DESTROY",
                    "/slash/slash/boom",
                    new Version(1, 1)
                ).Sequence()
            );
        }

        [Fact]
        public void RejectsUnspecifiedResourceWhenNotOptionsRequest()
        {
            Assert.Throws<ArgumentException>(() =>
                new RequestPrologue(
                    "GET",
                    new Version(1, 1)
                ).Sequence()
            );
        }

        [Fact]
        public void AllowsUnspecifiedResourceWhenOptionsRequest()
        {
            Assert.Equal(
                "OPTIONS * HTTP/1.1",
                string.Join(" ",
                    new RequestPrologue(
                        "OPTIONS",
                        new Version(1, 1)
                    ).Sequence()
                )
            );
        }
    }
}

