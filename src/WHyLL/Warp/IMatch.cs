using Tonga;
using WHyLL;

namespace WHyLL.Warp
{
    public interface IMatch<TOutput>
    {
        bool Matches(IPrologue prologue, IEnumerable<IPair<string, string>> parts, Stream body);
        IWarp<TOutput> Consequence(IPrologue prologue, IEnumerable<IPair<string, string>> parts, Stream body);
    }
}