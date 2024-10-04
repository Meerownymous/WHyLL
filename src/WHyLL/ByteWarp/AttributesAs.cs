using Tonga;

namespace WHyLL.ByteWarp;

public sealed class AttributesAs<TOutput>(
    Func<IEnumerable<IPair<string,byte[]>>, Task<TOutput>> render
) : ByteWarpEnvelope<TOutput>(
    new AsByteWarp<TOutput>(attributes => render(attributes))
);