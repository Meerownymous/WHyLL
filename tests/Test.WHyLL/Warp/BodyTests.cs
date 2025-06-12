using Tonga;
using Tonga.IO;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp;

public sealed class BodyTests
{
	[Fact]
	public async Task RendersBody()
	{
		Assert.Equal(
			"I have a killer body",
			await
				new Body()
					.Refine(new AsConduit("I have a killer body").Stream())
					.Render()
					.AsText()
					.Str()
		);
	}
}
