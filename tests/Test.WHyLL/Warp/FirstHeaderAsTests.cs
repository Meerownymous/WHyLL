using Tonga.Map;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FirstHeaderAsTests
	{
		[Fact]
		public async Task RendersFirstHeaderAsOutputType()
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

		[Fact] public async Task RendersFallbackIfNotFound()
		{
			Assert.Equal(1000,
				await
					new FirstHeaderAs<int>("WhatTheHeaderIsGoingOn",
						_ => 0,
						1000
					)
					.Render()
			);
		}
	}
}