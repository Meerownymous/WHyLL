using System;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace WHyLL.Warp.Test
{
	public sealed class BodyTests
	{
		[Fact]
		public async void RendersBody()
		{
			Assert.Equal(
				"I have a killer body",
				AsText._(
					(await
						new Body()
							.Refine(new AsInput("I have a killer body").Stream())
							.Render()
					)
				).AsString()
			);
		}
	}
}

