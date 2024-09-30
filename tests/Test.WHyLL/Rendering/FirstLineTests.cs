using Tonga.Text;
using Xunit;

namespace WHyLL.Warp.Test
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