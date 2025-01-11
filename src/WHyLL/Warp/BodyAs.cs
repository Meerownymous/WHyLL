namespace WHyLL.Warp
{
    /// <summary>
    /// Render the body of a message as output type.
    /// </summary>
    public sealed class BodyAs<Output> : WarpEnvelope<Output>
    {
        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        public BodyAs(Func<Stream, Output> render) : base(
            new PiecesAs<Output>((_, _, body) => 
            {
                if(body.CanSeek)
                    body.Seek(0, SeekOrigin.Begin);
                return Task.FromResult(render(body));
            })
        )
        { }

        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        public BodyAs(Func<Stream, Task<Output>> render) : base(
            new PiecesAs<Output>((_,_,body) => render(body))
        )
        { }
    }
}

