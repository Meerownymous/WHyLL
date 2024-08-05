using Tonga.Scalar;

namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name.
    /// </summary>
    public sealed class FirstHeader(string name) : RenderingEnvelope<string>(
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

