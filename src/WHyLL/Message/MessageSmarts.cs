using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message;

public static partial class MessageSmarts
{
    /// <summary>
    /// Message with firstline from text.
    /// </summary>
    public static IMessage With(this IMessage msg, IPrologue prologue) =>
        msg.With(prologue);

    /// <summary>
    /// Message with new headers.
    /// </summary>
    public static IMessage With(this IMessage msg, params IPair<string, string>[] newParts) =>
        msg.With(newParts.AsEnumerable());
}