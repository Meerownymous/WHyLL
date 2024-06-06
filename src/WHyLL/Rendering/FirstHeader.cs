using Tonga.Scalar;

namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name.
    /// </summary>
    public sealed class FirstHeader : RenderingEnvelope<string>
    {
        /// <summary>
        /// First header with the given name.
        /// </summary>
        public FirstHeader(string name) : base(
            new HeadersAs<string>(
                headers =>
                First._(
                    header => header.Key() == name,
                    headers,
                    new ArgumentException($"Header '{name}' does not exist.")
                ).Value()
                .Value()
            )
        )
        { }
    }
}

