using Newtonsoft.Json.Linq;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Body as json object.
    /// </summary>
    public sealed class BodyAsJson() : WarpEnvelope<JObject>(
        new BodyAs<JObject>(async bodyStream =>
            JObject.Parse(await new BodyAsString().Refine(bodyStream).Render())
        )
    );
}

namespace  WHyLL
{
    public static class BodyAsJsonSmarts
    {
        /// <summary>
        /// Body as Output using given render function.
        /// </summary>
        public static Task<JObject> BodyAsJson(this IMessage message) => 
            message.To(new BodyAsJson());
    }
}