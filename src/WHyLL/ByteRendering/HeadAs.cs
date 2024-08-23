namespace WHyLL.ByteRendering;

public sealed class HeadAs<TOutput>(Func<byte[], Task<TOutput>> render) : ByteRenderingEnvelope<TOutput>(
    new AsByteRendering<TOutput>(bytes => render(bytes))
);