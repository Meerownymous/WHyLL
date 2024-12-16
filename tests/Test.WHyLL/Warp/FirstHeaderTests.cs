using Tonga.Map;
using Tonga.Text;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FirstHeaderTests
	{
		[Fact]
		public async Task RendersFirstHeader()
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
		public async Task IgnoresCase()
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
		
		[Fact]
		public async Task UsesFallback()
		{
			Assert.Equal(
				"Pooh",
				AsText._(
					await
						new FirstHeader("CHarAcTEr", "Pooh")
							.Render()
					
				).AsString()
			);
		}
		
		[Fact]
		public async Task DoesNotUseFallbackWhenHeaderExists()
		{
			Assert.Equal(
				"James",
				AsText._(
					await
						new FirstHeader("character", "Pooh")
							.Refine(AsPair._("character", "James"))
							.Render()
					
				).AsString()
			);
		}
	}
}