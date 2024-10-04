using Tonga.Map;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FirstHeaderAsTests
	{
		[Fact]
		public async void RendersFirstHeaderAsOutputType()
		{
			var time = DateTime.Today;
			Assert.Equal(time,
				(await
					new FirstHeaderAs<DateTime>("Since", DateTime.Parse)
						.Refine(AsPair._("Since", time.ToLongTimeString()))
                        .Render()
				)
			);
		}
	}
}