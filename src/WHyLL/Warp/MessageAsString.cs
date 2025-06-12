using System.Text;
using WHyLL;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// a <see cref="IMessage"/> formatted as string />
    /// </summary>
    public sealed class MessageAsString() : WarpEnvelope<string>(
        new MessageAs<string>(async msg =>
            {
                StringBuilder sb = new();
                sb.AppendLine((await msg.To(new Prologue())).Trim());
                foreach (var headers in (await msg.To(new AllHeaders())).Pairs())
                {
                    foreach (var value in headers.Value())
                    {
                        sb.AppendLine($"{headers.Key()}:{value}");
                    }
                }

                sb.AppendLine();
                var body = await msg.To(new BodyAsString());
                if (body.Length > 0)
                    sb.AppendLine(body);
                return sb.ToString();
            }
        )
    );
}

namespace WHyLL
{
    public static class MessageAsStringSmarts
    {
        public static Task<string> AsString(this IMessage msg) => msg.To(new MessageAsString());
    }
}