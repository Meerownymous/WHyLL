namespace WHyLL.ByteRendering;

public sealed class BodyAs<TOutput>(Func<Stream, Task<TOutput>> render) : ByteRenderingEnvelope<TOutput>(
    new AsByteRendering<TOutput>(body => render(body))
);