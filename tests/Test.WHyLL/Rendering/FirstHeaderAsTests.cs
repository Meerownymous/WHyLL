using Tonga.Map;
using Tonga.Text;
using Xunit;

namespace WHyLL.Warp.Test
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
		
		[Fact]
		public async void IgnoresCase()
		{
			Assert.Equal(
				"Valu the Bear",
				AsText._(
					(await
						new FirstHeader("CHarAcTEr")
							.Refine(AsPair._("character", "Valu the Bear"))
							.Render()
					)
				).AsString()
			);
		}
	}
}