using Newtonsoft.Json.Linq;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Body as json array.
    /// </summary>
    public sealed class BodyAsJsonArray() : WarpEnvelope<JArray>(
        new BodyAs<JArray>(async bodyStream =>
            JArray.Parse(await new BodyAsString().Refine(bodyStream).Render())
        )
    );
}

namespace  WHyLL
{
    public static class BodyAsJsonArraySmarts
    {
        /// <summary>
        /// Body as Output using given render function.
        /// </summary>
        public static Task<JArray> BodyAsJsonArray(this IMessage message) => 
            message.To(new BodyAsJsonArray());
    }
}