using System.Text;

namespace WHyLL.Warp;

/// <summary>
/// a <see cref="IMessage"/> formatted as string />
/// </summary>
public sealed class MessageAsText : WarpEnvelope<string>
{
    /// <summary>
    /// a <see cref="IMessage"/> formatted as string />
    /// </summary>
    public MessageAsText() : base(
        new MessageAs<string>(async msg =>
            {
                StringBuilder sb = new();
                sb.AppendLine((await msg.To(new FirstLine())).Trim());
                foreach (var headers in (await msg.To(new AllHeaders())).Pairs())
                {
                    foreach (var value in headers.Value())
                    {
                        sb.AppendLine($"{headers.Key()}:{value}");
                    }
                }
                sb.AppendLine();
                var body = await msg.To(new BodyAsText());
                if(body.Length > 0)
                    sb.AppendLine(body);
                return sb.ToString();
            }
        )
    )
    { }
}