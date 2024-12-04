using Tonga;

namespace WHyLL.ByteWarp;

public sealed class AsByteWarp<TTarget>(Func<byte[], IEnumerable<IPair<string, byte[]>>, Stream, Task<TTarget>> render) : IByteWarp<TTarget>
{
    public AsByteWarp(Func<byte[], Task<TTarget>> render) : this(
        (head, _, _) => render(head)
    )
    { }
    
    public AsByteWarp(Func<IEnumerable<IPair<string,byte[]>>, Task<TTarget>> render) : this(
        (_, attributes, _) => render(attributes)
    )
    { }
    
    public AsByteWarp(Func<Stream, Task<TTarget>> render) : this(
        (_, _, body) => render(body)
    )
    { }
    
    public Task<TTarget> Render(byte[] head, IEnumerable<IPair<string, byte[]>> attributes, Stream body) =>
        render(head, attributes, body);
}