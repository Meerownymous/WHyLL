using System;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Whyre.Headers;
using Whyre.MessageInput;
using Whyre.Rendering;
using Xunit;

namespace Whyre.Message.Test
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
									new AsBytes("Inert me")
								).Stream()
							)
						).Render(new Body())
				).AsString()
            );
        }
    }
}

