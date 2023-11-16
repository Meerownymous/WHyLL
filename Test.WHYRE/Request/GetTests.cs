using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Text;
using Whyre.Parts;
using Whyre.Wire;
using Whyre.Test;
using Xunit;

namespace Whyre.Request.Test
{
    public sealed class GetTests
    {
        [Fact]
        public async void RefinesByParts()
        {
            Assert.Equal(
                "successful",
                First._(
                    (await
                        new Get(new Uri("http://www.enhanced-calm.com"))
                            .With(new Header("x-test", "successful"))
                            .Render(new Headers())
                    )["x-test"]
                ).Value()
            );
        }

        [Fact]
        public async void RefinesByRequestInput()
        {
            Assert.Equal(
                "successful",
                First._(
                    (await
                        new Get(new Uri("http://www.enhanced-calm.com"))
                            .With(new Header("x-test", "successful"))
                            .Render(new Headers())
                    )["x-test"]
                ).Value()
            );
        }

        [Fact]
        public async void RefinesByBody()
        {
            Assert.Equal(
                "success",
                AsText._(
                    await
                        new Get(new Uri("http://www.enhanced-calm.com"))
                            .WithBody(new MemoryStream(AsBytes._("success").Bytes()))
                            .Render(new Response())
                            .Render(new Body())
                ).AsString()
            );
        }
    }
}