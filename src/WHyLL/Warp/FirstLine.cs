using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Render the first line of a request (request-line) or response (status-line)
    /// </summary>
    public sealed class FirstLine() : WarpEnvelope<string>(
        new FirstLineAs<string>(line => line)
    );

}

namespace WHyLL
{
    public static class FirstLineSmarts
    {
        /// <summary>
        /// Firstline of the message.
        /// </summary>
        public static Task<string> FirstLine(this IMessage message) => 
            message.To(new FirstLine());
    }
}