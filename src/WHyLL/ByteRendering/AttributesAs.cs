using Tonga;

namespace WHyLL.ByteRendering;

public sealed class AttributesAs<TOutput>(
    Func<IEnumerable<IPair<string,byte[]>>, Task<TOutput>> render
) : ByteRenderingEnvelope<TOutput>(
    new AsByteRendering<TOutput>(attributes => render(attributes))
);