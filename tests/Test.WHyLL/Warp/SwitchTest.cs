using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class SwitchTest
    {
        [Fact]
        public async Task FindsBranch()
        {
            Assert.Equal(
                "GET /testresult HTTP/1.1",
                (await 
                    new Switch<string>(
                        new Case<string>(prologue => prologue.Sequence()[0] == "POST" , new Prologue()),
                        new Case<string>(prologue => prologue.Sequence()[0] == "GET", new Prologue())
                    )
                    .Refine(new AsPrologue(["GET", "/testresult", "HTTP/1.1"]))
                    .Render()
                )
            );
        }

        [Fact]
        public async Task RendersOnlyFirstMatch()
        {
            Assert.Equal(
                "2",
                await
                    new Switch<string>(
                        new Case<string>(p => p.Sequence()[0] == "POST", new FromScratch<string>("1")),
                        new Case<string>(p => p.Sequence()[0] == "GET", new FromScratch<string>("2")),
                        new Case<string>(p => p.Sequence()[0] == "GET",
                            new FromScratch<string>(render: () => throw new Exception())
                        )
                    )
                    .Refine(new AsPrologue(["GET", "/testresult", "HTTP/1.1"]))
                    .Render()
            );
        }

        [Fact]
        public async Task NoMatchDoesNotRender()
        {
            var rendered = false;

            try
            {
                await
                    new Switch<string>(
                        new Case<string>(p => p.Sequence()[0] == "POST",
                            new FromScratch<string>(() => { rendered = true; return "boom"; })
                        )
                    )
                    .Refine(new AsPrologue(["GET", "/testresult", "HTTP/1.1"]))
                    .Render();
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.False(rendered);
        }

        [Fact]
        public async void RaisesWhenNoTargetBranchFound()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    new Switch<string>(
                        new Case<string>(p => p.Sequence()[0] == "POST", new Prologue()),
                        new Case<string>(p => p.Sequence()[0] == "GET", new Prologue())
                    )
                    .Refine(new AsPrologue(["PUT", "/testresult", "HTTP/1.1"]))
                    .Render()
            );
        }
    }
}

