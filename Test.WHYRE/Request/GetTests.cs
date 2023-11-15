using System;
using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Text;
using Whyre;
using Whyre.Parts;
using Whyre.Request;
using WHYRE.Test;
using Xunit;

namespace Test.WHYRE.Request
{
	public sealed class GetTests
	{
		[Fact]
		public async void RefinesByParts()
		{
			var request =
				new Get(
                    new BounceRequest())
					.Refined(new Header("x-test", "successful"));

			Assert.Equal(
				"successful",
				First._((await request.Render(new ResponseHeaders()))["x-test"]).Value()
			);
		}

        [Fact]
        public async void RefinesByRequestInput()
        {
            var request =
                new Get(new BounceRequest())
                    .Refined(
						AsRequestInput._(
							new Header("x-test", "successful")
						)
					);

            Assert.Equal(
                "successful",
                First._((await request.Render(new ResponseHeaders()))["x-test"]).Value()
            );
        }

        [Fact]
        public async void RefinesByBody()
        {
            var request =
                new Get(new BounceRequest())
                    .Refine(
                        new MemoryStream(AsBytes._("success").Bytes())
                    );

            Assert.Equal(
                "success",
                AsText._(await request.Render(new ResponseBody())).AsString()
            );
        }
    }
}