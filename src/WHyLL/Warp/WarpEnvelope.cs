using Tonga;

namespace WHyLL.Warp;

/// <summary>
/// Envelope for Warps.
/// </summary>
public abstract class WarpEnvelope<TOutput>(IWarp<TOutput> origin) : IWarp<TOutput>
{
    public IWarp<TOutput> Refine(IPrologue newPrologue) =>
        origin.Refine(newPrologue);

    public IWarp<TOutput> Refine(IEnumerable<IPair<string, string>> newParts) =>
        this.Refine(newParts.ToArray());

    public IWarp<TOutput> Refine(params IPair<string, string>[] parts) =>
        origin.Refine(parts);

    public IWarp<TOutput> Refine(Stream newBody) =>
        origin.Refine(newBody);

    public Task<TOutput> Render() =>
        origin.Render();
}

