namespace WHyLL.Rendering
{
    public sealed class Switch<TOutput> : RenderingEnvelope<TOutput>
    {
        public Switch(params IMatch<TOutput>[] branches) : base(
            new FromPieces<TOutput>((firstLine, parts, body) =>
            {
                Task<TOutput> result = default(Task<TOutput>);
                bool matched = false;
                foreach(var match in branches)
                {
                    matched = match.Matches(firstLine, parts, body);
                    result = match.Consequence(firstLine, parts, body).Render();
                    if (matched) break;
                }
                if (!matched)
                    throw new InvalidOperationException($"No target rendering found.");
                return result;
            })
        )
        { }
    }
}

