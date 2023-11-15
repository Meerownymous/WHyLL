using System;
using Whyre.Parts;
using Xunit;

namespace WHYRE.Parts.Test
{
	public sealed class RequestLineTests
	{
		[Theory]
		[InlineData("GET")]
		public void MakesRelativeResourceLine(string method)
		{
			Assert.Equal(
				$"{method} /slash/slash/boom HTTP/1.1\r\n",
				new RequestLine(
					"GET",
					new Uri("/slash/slash/boom"),
					new Version(1, 1)
				).Value()
			);
		}
	}
}

