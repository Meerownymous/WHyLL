using Xunit;

namespace WHyLL.Http.Headers.Tests
{
	public sealed class BasicAuthTests
	{
        [Fact]
        public void HasHeaderName()
        {
            Assert.Equal(
                "Authorization",
                new BasicAuth("user", "password").Key()
            );
        }

        [Fact]
		public void EncodesCredentials()
		{
			Assert.Equal(
                "Basic dXNlcjpwYXNzd29yZA==",
				new BasicAuth("user", "password").Value()
			);
        }
	}
}

