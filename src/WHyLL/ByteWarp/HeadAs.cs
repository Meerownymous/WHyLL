namespace WHyLL.ByteWarp;

public sealed class HeadAs<TOutput>(Func<byte[], Task<TOutput>> render) : ByteWarpEnvelope<TOutput>(
    new AsByteWarp<TOutput>(bytes => render(bytes))
);