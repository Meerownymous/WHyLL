using System;
using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Text;
using Whyre;
using Whyre.Parts;
using Whyre.Request;
using Whyre.Wire;
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
                new Get(new Uri("www.enhanced-calm.com"))
                    .Refined(new Header("x-test", "successful"));

            Assert.Equal(
                "successful",
                First._((await request.Render(new Headers()))["x-test"]).Value()
            );
        }

        [Fact]
        public async void RefinesByRequestInput()
        {
            var request =
                new Get(new Uri("www.enhanced-calm.com"))
                    .Refined(
                        new Header("x-test", "successful")
                    );

            Assert.Equal(
                "successful",
                First._((await request.Render(new Headers()))["x-test"]).Value()
            );
        }

        [Fact]
        public async void RefinesByBody()
        {
            var body =
                await new Get(new Uri("http://www.enhanced-calm.com"))
                    .Refine(new MemoryStream(AsBytes._("success").Bytes()))
                    .Render(new Response())
                    .Render(new Body());

            Assert.Equal(
                "success",
                AsText._(body).AsString()
            );
        }
    }
}