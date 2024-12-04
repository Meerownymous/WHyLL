using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message;

public static class MessageSmarts
{
    /// <summary>
    /// Message with firstline from text.
    /// </summary>
    public static IMessage With(this IMessage msg, IText firstLine) =>
        msg.With(firstLine.AsString());

    /// <summary>
    /// Message with new headers.
    /// </summary>
    public static IMessage With(this IMessage msg, params IPair<string, string>[] newParts) =>
        msg.With(new AsEnumerable<IPair<string,string>>(newParts));
}