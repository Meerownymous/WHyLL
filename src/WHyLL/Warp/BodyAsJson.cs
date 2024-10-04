using Newtonsoft.Json.Linq;

namespace WHyLL.Warp;

/// <summary>
/// Body as json object.
/// </summary>
public sealed class BodyAsJson() : WarpEnvelope<JObject>(
    new BodyAs<JObject>(async bodyStream =>
        JObject.Parse(await new BodyAsText().Refine(bodyStream).Render())  
    )
);