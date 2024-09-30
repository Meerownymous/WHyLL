using Tonga;
using WHyLL;

namespace WHyLL.Warp
{
    public interface IMatch<TOutput>
    {
        bool Matches(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body);
        IWarp<TOutput> Consequence(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body);
    }
}