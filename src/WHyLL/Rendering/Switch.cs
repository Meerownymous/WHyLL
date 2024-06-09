namespace WHyLL.Rendering
{
    /// <summary>
    /// Switches based on a given match function, if a rendering should happen.
    /// Renders that one, and no following.
    /// </summary>
    public sealed class Switch<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// Switches based on a given match function, if a rendering should happen.
        /// Renders that one, and no following.
        /// </summary>
        public Switch(params IMatch<TOutput>[] branches) : base(
            new PiecesAs<TOutput>(async (firstLine, parts, body) =>
            {
                TOutput result = default(TOutput);
                bool matched = false;
                foreach(var match in branches)
                {
                    matched = match.Matches(firstLine, parts, body);
                    if (matched)
                    {
                        result = await match.Consequence(firstLine, parts, body).Render();
                        break;
                    }
                }
                if (!matched)
                    throw new InvalidOperationException($"No target rendering found.");
                return result;
            })
        )
        { }
    }
}

