using Tonga;
using Tonga.Enumerable;
using WHyLL.Headers;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Message
{
    public sealed class SimpleMessageTests
	{
		[Fact]
		public async Task IncludesFirstLine()
		{
			Assert.Equal(
                "POST /kasten HTTP/1.1",
                await
					new SimpleMessage("POST /kasten HTTP/1.1", None._<IPair<string,string>>(), new MemoryStream())
						.To(new FirstLine())
			);
		}

        [Fact]
        public async Task RefinesFirstLine()
        {
            Assert.Equal(
                "PUT /putput/dear/pigeon HTTP/1.1",
                await
                    new SimpleMessage(
                        "POST /kasten HTTP/1.1",
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    )
                    .With("PUT /putput/dear/pigeon HTTP/1.1")
                    .To(new FirstLine())
            );
        }

        [Fact]
        public async Task IncludesHeader()
        {
            Assert.Contains(
                "Baseball-Cap",
                    (await
                        new SimpleMessage(
                            "POST /kasten HTTP/1.1",
                            Tonga.Enumerable.Single._(new Header("Accepted-Headwear", "Baseball-Cap")),
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
                        "POST /kasten HTTP/1.1",
                        Tonga.Enumerable.Single._(new Header("Accepted-Headwear", "Baseball-Cap")),
                        new MemoryStream()
                    )
                    .With(new Header("Accepted-Headwear", "Underpants"))
                    .To(new AllHeaders())
                )["Accepted-Headwear"]
            );
        }
    }
}

