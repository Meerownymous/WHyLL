using Tonga.Text;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="String"/>
    /// </summary>
    public sealed class BodyAsText : RenderingEnvelope<String>
    {
        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        public BodyAsText() : base(
            new PiecesAs<string>((x,y,body) =>
                Task.FromResult(AsText._(body).AsString())
            )
        )
        { }
    }
}

