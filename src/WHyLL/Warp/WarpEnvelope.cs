using Tonga;

namespace WHyLL.Warp;

/// <summary>
/// Envelope for Warps.
/// </summary>
public abstract class WarpEnvelope<TOutput>(IWarp<TOutput> origin) : IWarp<TOutput>
{
    public IWarp<TOutput> Refine(string start) =>
        origin.Refine(start);

    public IWarp<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
        this.Refine(parts.ToArray());

    public IWarp<TOutput> Refine(params IPair<string, string>[] parts) =>
        origin.Refine(parts);

    public IWarp<TOutput> Refine(Stream body) =>
        origin.Refine(body);

    public Task<TOutput> Render() =>
        origin.Render();
}

