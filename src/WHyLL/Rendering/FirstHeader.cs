using Tonga.Scalar;

namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name.
    /// </summary>
    public sealed class FirstHeader(string name) : RenderingEnvelope<string>(
        new FirstHeaderAs<string>(name, header => header)
    )
    { }
}

