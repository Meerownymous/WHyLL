using WHyLL.Http.Warp;
using WHyLL.Warp;

namespace WHyLL.Http.Warp
{

    /// <summary>
    /// Renders content-type header as string.
    /// </summary>
    public sealed class ContentType() : WarpEnvelope<string>(
        new FirstHeader("content-type")
    );
}

namespace WHyLL.Http
{
    public static class ContentTypeSmarts
    {
        public static Task<string> ContentType(this IMessage msg) => msg.To(new ContentType());
    }
}