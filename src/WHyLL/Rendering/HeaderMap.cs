using Tonga;
using Tonga.Map;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Headers mapped from name to values.
    /// </summary>
    public sealed class HeaderMap(IEnumerable<IPair<string,string>> headers) : 
        MapEnvelope<string, ICollection<string>>(
            new AsMap<string, ICollection<string>>(() =>
                {
                    var result = new AsMap<string, ICollection<string>>();
                    foreach (var header in headers)
                    {
                        if (!result.Keys().Contains(header.Key()))
                        {
                            ICollection<string> list = new List<string>() { header.Value() };
                            result.With(AsPair._(header.Key(), list));
                        }
                    }
                    return result.Pairs();
                }
            )
        )
    { }
}

