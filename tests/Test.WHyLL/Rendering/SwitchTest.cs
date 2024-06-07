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

