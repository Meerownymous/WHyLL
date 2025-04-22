using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// First header with the given name as int.
    /// </summary>
    public sealed class FirstHeaderAsInt(string name) : WarpEnvelope<int>(
        new FirstHeaderAs<int>(name, Convert.ToInt32)
    );
}

namespace  WHyLL
{
    public static class FirstHeaderAsIntSmarts
    {
        public static Task FirstHeaderAsInt(this IMessage message, string name) =>
            message.To(new FirstHeaderAsInt(name));
    }
}