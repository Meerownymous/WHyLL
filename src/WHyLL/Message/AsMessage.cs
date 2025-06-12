using Tonga;
using Tonga.Enumerable;
using WHyLL.Prologue;

namespace WHyLL.Message;

/// <summary>
/// Message from pieces. 
/// </summary>
public sealed class AsMessage(IPrologue prologue, IEnumerable<IPair<string,string>> parts, Stream body) : IMessage
{
    public async Task<T> To<T>(IWarp<T> warp) =>
        await
            warp.Refine(prologue)
                .Refine(parts)
                .Refine(body)
                .Render();

    public IMessage With(IPrologue newPrologue) =>
        new AsMessage(newPrologue, parts, body);

    public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
        new AsMessage(prologue, parts.AsJoined(newParts), body);

    public IMessage WithBody(Stream newBody) =>
        new AsMessage(prologue, parts, newBody);
}

public static partial class MessageSmarts
{
    /// <summary>
    /// Stream as <see cref="IMessage"/>
    /// </summary>
    public static IMessage AsMessage(this Stream body) =>
        new AsMessage(new Blank(), new None<IPair<string,string>>(), body);
    
    /// <summary>
    /// Stream as <see cref="IMessage"/>
    /// </summary>
    public static IMessage AsMessage(this Stream body, params IPair<string,string>[] headers) =>
        new AsMessage(new Blank(), headers, body);
    
    /// <summary>
    /// Stream as <see cref="IMessage"/>
    /// </summary>
    public static IMessage AsMessage(this Stream body, IPrologue prologue, params IPair<string,string>[] headers) =>
        new AsMessage(prologue, headers, body);
}