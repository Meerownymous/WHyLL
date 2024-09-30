namespace WHyLL.Warp
{
    /// <summary>
    /// Switches based on a given match function, if a Warp should happen.
    /// Renders that one, and no following.
    /// </summary>
    public sealed class Switch<TOutput>(params IMatch<TOutput>[] branches) : 
        WarpEnvelope<TOutput>(
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
                    throw new InvalidOperationException($"No target Warp found.");
                return result;
            })
        )
    { }
}

