﻿using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public sealed class PiecesAs<TOutput>(
        Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render,
        string firstLine,
        IEnumerable<IPair<string,string>> parts,
        Stream body
    ) : IRendering<TOutput>
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public PiecesAs(
            Func<string,IEnumerable<IPair<string,string>>,Stream, Task<TOutput>> render) : this(
            render, string.Empty, None._<IPair<string,string>>(), new MemoryStream()
        )
        { }

        public IRendering<TOutput> Refine(string start) =>
            new PiecesAs<TOutput>(render, start, parts, body);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] newParts) =>
            new PiecesAs<TOutput>(render, firstLine, Joined._(parts, newParts), body);

        public IRendering<TOutput> Refine(Stream newBody) =>
            new PiecesAs<TOutput>(render, firstLine, parts, newBody);

        public async Task<TOutput> Render() =>
            await render(firstLine, parts, body);
    }

    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public static class PiecesAs
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public static PiecesAs<TOutput> _<TOutput>(
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render
        ) =>
            new(render);

    }
}

