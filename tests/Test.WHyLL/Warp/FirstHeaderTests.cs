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
				await
					new FirstHeader("Character")
						.Refine(("Character", "Valu the Bear").AsPair())
                        .Refine(("Character", "Mister Murphy").AsPair())
                        .Render()
			);
		}
		
		[Fact]
		public async Task IgnoresCase()
		{
			Assert.Equal(
				"Valu the Bear", 
				await
					new FirstHeader("CHarAcTEr")
						.Refine(("character", "Valu the Bear").AsPair())
						.Render()
			);
		}
		
		[Fact]
		public async Task UsesFallback()
		{
			Assert.Equal(
				"Pooh",
				await
					new FirstHeader("CHarAcTEr", "Pooh")
						.Render()
			);
		}
		
		[Fact]
		public async Task DoesNotUseFallbackWhenHeaderExists()
		{
			Assert.Equal(
				"James",
				await
					new FirstHeader("character", "Pooh")
						.Refine(("character", "James").AsPair())
						.Render()
			);
		}
	}
}