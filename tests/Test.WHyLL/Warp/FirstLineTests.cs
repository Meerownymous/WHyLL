using Tonga.Text;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FirstLineTests
	{
		[Fact]
		public async void RendersFirstLine()
		{
			Assert.Equal(
				"CONNECT /world HTTP/1.1",
				AsText._(
					(await
						new FirstLine()
							.Refine("CONNECT /world HTTP/1.1")
							.Render()
					)
				).AsString()
			);
		}
	}
}