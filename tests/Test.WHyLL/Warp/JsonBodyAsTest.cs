using Newtonsoft.Json.Linq;
using Tonga.IO;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp;

public sealed class JsonBodyAsTest
{
    [Fact]
    public async Task OffersBodyAsJson()
    {
        var json = new JObject()
        {
            { "Greeting", "Hello" }
        };
        
        var offered = 
            await 
                new SimpleMessage()
                    .WithBody(new AsInputStream(json.ToString()))
                    .To(new JsonBodyAs<JObject>(j => j));
        
        Assert.Equal(json.ToString(), offered.ToString()); 
    }
}