namespace WHyLL.ByteWarp;

public sealed class BodyAs<TOutput>(Func<Stream, Task<TOutput>> render) : ByteWarpEnvelope<TOutput>(
    new AsByteWarp<TOutput>(body => render(body))
);