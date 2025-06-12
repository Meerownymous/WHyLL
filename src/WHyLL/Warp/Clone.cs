using Tonga;
using Tonga.Enumerable;
using WHyLL;
using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// A copy of the Warp message.
    /// </summary>
	public sealed class Clone(IPrologue prologue, IEnumerable<IPair<string, string>> parts, Stream body) : IWarp<IMessage>
	{
        /// <summary>
        /// A copy of the Warp message.
        /// </summary>
        public Clone() : this(new Blank(), new None<IPair<string, string>>(), new MemoryStream())
        { }

        public IWarp<IMessage> Refine(IPrologue newPrologue) =>
            new Clone(newPrologue, parts, body);

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string,string>[] newParts) =>
            new Clone(prologue, parts.AsJoined(newParts), body);

        public IWarp<IMessage> Refine(Stream newBody) =>
            new Clone(prologue, parts, newBody);

        public async Task<IMessage> Render()
        {
            var bodyClone = new MemoryStream();
            var pos = body.Position;
            body.Seek(0, SeekOrigin.Begin);
            await body.CopyToAsync(bodyClone);
            body.Seek(pos, SeekOrigin.Begin);
            return
                await Task.FromResult(
                    new SimpleMessage(prologue, parts, bodyClone)
                );
        }
    }
}

public static class CloneSmarts
{
    public static Task<IMessage> Clone(this IMessage message) => message.To(new Clone()); 
}