using System;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Whyre.Rendering.Test
{
	public sealed class FirstLineTests
	{
		[Fact]
		public async void RendersFirstLine()
		{
			Assert.Equal(
				"CONNECT /world HTTP/1.1",
				await
					new FirstLine()
						.Refine("CONNECT /world HTTP/1.1")
						.Render()
			);
		}
	}
}

