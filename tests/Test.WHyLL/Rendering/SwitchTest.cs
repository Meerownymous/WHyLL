using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class SwitchTest
    {
        [Fact]
        public async void FindsBranch()
        {
            Assert.Equal(
                "GET /testresult HTTP/1.1",
                (await 
                    new Switch<string>(
                        new Case<string>(firstLine => firstLine.StartsWith("POST"), new FirstLine()),
                        new Case<string>(firstLine => firstLine.StartsWith("GET"), new FirstLine())
                    )
                    .Refine("GET /testresult HTTP/1.1")
                    .Render()
                )
            );
        }

        [Fact]
        public async void RendersOnlyFirstMatch()
        {
            Assert.Equal(
                "2",
                (await
                    new Switch<string>(
                        new Case<string>(firstLine => firstLine.StartsWith("POST"), new Fixed<string>("1")),
                        new Case<string>(firstLine => firstLine.StartsWith("GET"), new Fixed<string>("2")),
                        new Case<string>(firstLine => firstLine.StartsWith("GET"),
                            new FromScratch<string>(render: () => throw new Exception())
                        )
                    )
                    .Refine("GET /testresult HTTP/1.1")
                    .Render()
                )
            );
        }

        [Fact]
        public async void NoMatchDoesNotRender()
        {
            var rendered = false;

            try
            {
                await
                    new Switch<string>(
                        new Case<string>(firstLine => firstLine.StartsWith("POST"),
                            new FromScratch<string>(() => { rendered = true; return "boom"; })
                        )
                    )
                    .Refine("GET /testresult HTTP/1.1")
                    .Render();
            }
            catch (Exception ex) { }
            Assert.False(rendered);
        }

        [Fact]
        public async void RaisesWhenNoTargetBranchFound()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    new Switch<string>(
                        new Case<string>(firstLine => firstLine.StartsWith("POST"), new FirstLine()),
                        new Case<string>(firstLine => firstLine.StartsWith("GET"), new FirstLine())
                    )
                    .Refine("PUT /testresult HTTP/1.1")
                    .Render()
            );
        }
    }
}

