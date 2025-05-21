using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Message;
using WHyLL.MessageInput;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Message
{
	public sealed class MessageWithInputsTests
	{
		[Fact]
		public async Task InsertsHeader()
		{
			Assert.Contains(
				"Approved",
				(await
					new MessageWithInputs(
						new HeaderInput(
							new Header("Elegance", "Approved")
						)
					).To(new AllHeaders())
				)["Elegance"]
			);
		}

        [Fact]
        public async Task InsertsBody()
        {
            Assert.Equal(
                "Insert me",
				AsText._(
					await
						new MessageWithInputs(
							new BodyInput(
								new AsConduit(
									new AsBytes("Insert me")
								).Stream()
							)
						).To(new Body())
				).AsString()
            );
        }
    }
}

