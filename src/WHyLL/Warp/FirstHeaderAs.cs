using Tonga.Scalar;

namespace WHyLL.Warp;

/// <summary>
/// First header with the given name as output type.
/// </summary>
public sealed class FirstHeaderAs<TOutput>(string name, Func<string,TOutput> mapping) : 
WarpEnvelope<TOutput>(
    new HeadersAs<TOutput>(headers =>
        mapping(
            First._(
                    header => header.Key().Equals(name, StringComparison.OrdinalIgnoreCase),
                    headers,
                    new ArgumentException($"Header '{name}' does not exist.")
                ).Value()
                .Value()
        )
    )
);