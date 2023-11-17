using Xunit;

namespace Whyre.Headers.Tests
{
	public sealed class ContentTypeTests
	{
        [Fact]
        public void HasHeaderName()
        {
            Assert.Equal(
                "Content-Type",
                new ContentType("application/foobar").Key()
            );
        }

        [Fact]
		public void IncludesContentType()
		{
			Assert.Equal(
                "application/json",
                new ContentType("application/json").Value()
			);
        }
	}
}

