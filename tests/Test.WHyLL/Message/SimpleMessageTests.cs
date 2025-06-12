using Tonga;
using Tonga.Enumerable;
using WHyLL.Headers;
using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Message
{
    public sealed class SimpleMessageTests
	{
		[Fact]
		public async Task IncludesPrologue()
		{
			Assert.Equal(
                "POST /kasten HTTP/1.1",
                await
					new SimpleMessage(
                        new AsPrologue(["POST", "/kasten", "HTTP/1.1"]), 
                        new None<IPair<string,string>>(), 
                        new MemoryStream()
                    ).To(new Prologue())
			);
		}

        [Fact]
        public async Task RefinesFirstLine()
        {
            Assert.Equal(
                "PUT /putput/dear/pigeon HTTP/1.1",
                await
                    new SimpleMessage(
                        new AsPrologue(["POST", "/kasten", "HTTP/1.1"]),
                        new None<IPair<string, string>>(),
                        new MemoryStream()
                    )
                    .With(new AsPrologue(["PUT", "/putput/dear/pigeon","HTTP/1.1"]))
                    .To(new Prologue())
            );
        }

        [Fact]
        public async Task IncludesHeader()
        {
            Assert.Contains(
                "Baseball-Cap",
                    (await
                        new SimpleMessage(
                            new AsPrologue(["POST", "/kasten", "HTTP/1.1"]),
                            new Header("Accepted-Headwear", "Baseball-Cap").AsSingle(),
                            new MemoryStream()
                        )
                        .To(new AllHeaders())
                    )["Accepted-Headwear"]
            );
        }

        [Fact]
        public async Task RefinesHeaders()
        {
            Assert.Contains(
                "Underpants",
                (await
                    new SimpleMessage(
                        new AsPrologue(["POST", "/kasten", "HTTP/1.1"]),
                        new Header("Accepted-Headwear", "Baseball-Cap").AsSingle(),
                        new MemoryStream()
                    )
                    .With(new Header("Accepted-Headwear", "Underpants"))
                    .To(new AllHeaders())
                )["Accepted-Headwear"]
            );
        }
    }
}

