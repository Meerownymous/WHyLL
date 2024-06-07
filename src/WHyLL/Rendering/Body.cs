using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="Stream"/>
    /// </summary>
    public sealed class Body : RenderingEnvelope<Stream>
    {
        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        public Body() : base(new BodyAs<Stream>(body => body))
        { }
    }
}

