using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render the body of a message as output type.
    /// </summary>
    public sealed class BodyAs<Output> : RenderingEnvelope<Output>
    {
        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        public BodyAs(Func<Stream, Output> render) : base(
            new PiecesAs<Output>((x, y, body) => Task.FromResult(render(body)))
        )
        { }

        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        public BodyAs(Func<Stream, Task<Output>> render) : base(
            new PiecesAs<Output>((x,y,body) => render(body))
        )
        { }
    }
}

