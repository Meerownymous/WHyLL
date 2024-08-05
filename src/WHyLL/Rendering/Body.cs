namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="Stream"/>
    /// </summary>
    public sealed class Body() : RenderingEnvelope<Stream>(new BodyAs<Stream>(body => body))
    { }

    /// <summary>
    /// Renders the body of a message as <see cref="Stream"/>
    /// </summary>
    public static partial class MessageExtension
    {
        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        public static async Task<Stream> Body(this IMessage message) =>
            await message.Render(new Body());
    }
}

