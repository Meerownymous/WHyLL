using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp;

public sealed class ConditionalTests
{
    [Fact]
    public async Task DelegatesToYes()
    {
        Assert.Equal(100,
            await 
                new Conditional<int>(
                    _ => true,
                    new BodyAs<int>(_ => 100),
                    new BodyAs<int>(_ => 0)
                ).Render()
        );
    }
    
    [Fact]
    public async Task DelegatesToNo()
    {
        Assert.Equal(50,
            await 
                new Conditional<int>(
                    _ => false,
                    new BodyAs<int>(_ => 100),
                    new BodyAs<int>(_ => 50)
                ).Render()
        );
    }
}