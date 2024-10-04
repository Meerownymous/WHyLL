using Newtonsoft.Json.Linq;
using WHyLL.Warp;

/// <summary>
/// Body as json array.
/// </summary>
public sealed class BodyAsJsonArray() : WarpEnvelope<JArray>(
    new BodyAs<JArray>(async bodyStream =>
        JArray.Parse(await new BodyAsText().Refine(bodyStream).Render())  
    )
);