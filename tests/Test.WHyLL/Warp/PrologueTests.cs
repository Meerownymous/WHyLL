using Tonga.Text;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class PrologueTests
	{
		[Fact]
		public async void RendersFirstLine()
		{
			Assert.Equal(
				"CONNECT /world HTTP/1.1",
				await
					new Prologue()
						.Refine(new AsPrologue(["CONNECT", "/world","HTTP/1.1"]))
						.Render()
			);
		}
	}
}