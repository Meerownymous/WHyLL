using Tonga;
using Tonga.Collection;

namespace WHyLL.ByteMessage;

public sealed class AsByteMessage(byte[] head, IEnumerable<IPair<string,byte[]>> attributes, Stream body) : IByteMessage
{
    public IByteMessage With(byte[] newHead) => new AsByteMessage(head, attributes, body);

    public IByteMessage With(IEnumerable<IPair<string, byte[]>> newAttributes) =>
        new AsByteMessage(head, Joined._(attributes, newAttributes), body);

    public IByteMessage WithBody(Stream newBody) =>
        new AsByteMessage(head, attributes, body);

    public Task<T> Render<T>(IByteWarp<T> Warp) =>
        Warp.Render(head, attributes, body);
}