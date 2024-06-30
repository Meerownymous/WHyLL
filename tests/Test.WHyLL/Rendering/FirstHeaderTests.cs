using Tonga.Map;
using Tonga.Text;
using Xunit;

namespace WHyLL.Rendering.Test
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