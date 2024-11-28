using WHyLL;
using WHyLL.Warp;

public static class MessageSmarts
{
    /// <summary>
    /// Allows fluent async message warping.
    /// </summary>
    public static Task<TOutput> To<TOutput>(
        this IMessage message, Func<IMessage, Task<TOutput>> rendering
    ) =>
        message.To(new MessageAs<TOutput>(rendering));
}