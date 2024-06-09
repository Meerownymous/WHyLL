using Tonga;
using Tonga.Enumerable;
using WHyLL.Headers;
using WHyLL.Rendering;
using Xunit;

namespace WHyLL.Message.Test
{
    public sealed class SimpleMessageTests
	{
		[Fact]
		public async void IncludesFirstLine()
		{
			Assert.Equal(
                "POST /kasten HTTP/1.1",
                await
					new SimpleMessage("POST /kasten HTTP/1.1", None._<IPair<string,string>>(), new MemoryStream())
						.Render(new FirstLine())
			);
		}

        [Fact]
        public async void RefinesFirstLine()
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
                    .Render(new FirstLine())
            );
        }

        [Fact]
        public async void IncludesHeader()
        {
            Assert.Contains(
                "Baseball-Cap",
                    (await
                        new SimpleMessage(
                            "POST /kasten HTTP/1.1",
                            Tonga.Enumerable.Single._(new Header("Accepted-Headwear", "Baseball-Cap")),
                            new MemoryStream()
                        )
                        .Render(new AllHeaders())
                    )["Accepted-Headwear"]
            );
        }

        [Fact]
        public async void RefinesHeaders()
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
                    .Render(new AllHeaders())
                )["Accepted-Headwear"]
            );
        }
    }
}

