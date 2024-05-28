using System;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.MessageInput;
using WHyLL.Rendering;
using Xunit;

namespace WHyLL.Message.Test
{
	public sealed class MessageOfInputsTests
	{
		[Fact]
		public async void InsertsHeader()
		{
			Assert.Contains(
				"Approved",
				(await
					new MessageOfInputs(
						new HeaderInput(
							new Header("Elegance", "Approved")
						)
					).Render(new AllHeaders())
				)["Elegance"]
			);
		}

        [Fact]
        public async void InsertsBody()
        {
            Assert.Equal(
                "Insert me",
				AsText._(
					await
						new MessageOfInputs(
							new BodyInput(
								new AsInput(
									new AsBytes("Insert me")
								).Stream()
							)
						).Render(new Body())
				).AsString()
            );
        }
    }
}

