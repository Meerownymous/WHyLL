using Tonga;
using WHyLL;

namespace WHyLL.Rendering
{
    public interface IMatch<TOutput>
    {
        bool Matches(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body);
        IRendering<TOutput> Consequence(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body);
    }
}