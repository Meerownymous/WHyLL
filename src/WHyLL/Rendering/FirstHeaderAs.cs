using Tonga.Scalar;

namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name as output type.
    /// </summary>
    public sealed class FirstHeaderAs<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// First header with the given name as output type.
        /// </summary>
        public FirstHeaderAs(string name, Func<string,TOutput> mapping) : base(
            new HeadersAs<TOutput>(headers =>
                mapping(
                    First._(
                        header => header.Key() == name,
                        headers,
                        new ArgumentException($"Header '{name}' does not exist.")
                    ).Value()
                    .Value()
                )
            )
        )
        { }
    }
}

