using Tonga.Map;
using Tonga.Text;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class FirstHeaderTests
	{
		[Fact]
		public async void RendersFirstHeader()
		{
			Assert.Equal(
				"Valu the Bear",
				AsText._(
					(await
						new FirstHeader("Character")
							.Refine(AsPair._("Character", "Valu the Bear"))
                            .Refine(AsPair._("Character", "Mister Murphy"))
                            .Render()
					)
				).AsString()
			);
		}
	}
}