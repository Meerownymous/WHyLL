﻿using WHyLL.Headers;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
	public sealed class AllHeadersTests
	{
		[Theory]
		[InlineData("Test-Type", "Unit")]
        [InlineData("Test-Difficulty", "Easy")]
        [InlineData("Test-Difficulty", "Peasy")]
        public async void RendersHeaders(string key, string value)
		{
			Assert.Contains(
				value,
				(await
					new AllHeaders()
						.Refine(new Header("Test-Type", "Unit"))
                        .Refine(new Header("Test-Difficulty", "Easy"))
                        .Refine(new Header("Test-Difficulty", "Peasy"))
                        .Render()
				)[key]
			);
		}
	}
}

