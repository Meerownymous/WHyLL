using Tonga;

namespace WHyLL.Message;

public static class MessageSmarts
{
    /// <summary>
    /// Message with firstline from text.
    /// </summary>
    public static IMessage With(this IMessage msg, IText firstLine) =>
        msg.With(firstLine.AsString());
}