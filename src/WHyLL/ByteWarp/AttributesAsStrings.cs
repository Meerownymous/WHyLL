using System.Text;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;

namespace WHyLL.ByteWarp;

public sealed class AttributesAsStrings(Encoding enc) : ByteWarpEnvelope<IEnumerable<IPair<string, string>>>(
    new AttributesAs<IEnumerable<IPair<string, string>>>(
        attributes => Task.FromResult(
            Mapped._(
                attribute => AsPair._(attribute.Key(), () => enc.GetString(attribute.Value())),
                attributes
            )
        )
    )
)
{
    public AttributesAsStrings() : this(Encoding.UTF8)
    { }
}