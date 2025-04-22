using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// First header with the given name as output type.
    /// </summary>
    public sealed class FirstHeaderAs<TOutput>(string name, Func<string, TOutput> mapping, Func<TOutput> fallback) :
        WarpEnvelope<TOutput>(
            new HeadersAs<TOutput>(headers =>
            {
                TOutput result = default;
                bool found = false;
                foreach (var header in headers)
                {
                    if (header.Key().Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        result = mapping(header.Value());
                        found = true;
                        break;
                    }
                }

                if (!found)
                    result = fallback();
                return result;
            })
        )
    {
        /// <summary>
        /// First header with the given name as output type.
        /// </summary>
        public FirstHeaderAs(string name, Func<string, TOutput> mapping, TOutput fallback) : this(name, mapping,
            () => fallback)
        {
        }

        /// <summary>
        /// First header with the given name as output type.
        /// </summary>
        public FirstHeaderAs(string name, Func<string, TOutput> mapping) : this(name, mapping,
            () => throw new ArgumentException($"Header '{name}' does not exist"))
        {
        }
    }
}

namespace  WHyLL
{
    public static class FirstHeaderAsSmarts
    {
        public static Task<TOutput> FirstHeaderAs<TOutput>(this IMessage message, string name, Func<string, TOutput> mapping, Func<TOutput> fallback) =>
            message.To(new FirstHeaderAs<TOutput>(name, mapping, fallback));
        
        public static Task<TOutput> FirstHeaderAs<TOutput>(this IMessage message, string name, Func<string, TOutput> mapping) =>
            message.To(new FirstHeaderAs<TOutput>(name, mapping));
        
        public static Task<TOutput> FirstHeaderAs<TOutput>(this IMessage message, string name, Func<string, TOutput> mapping, TOutput fallback) =>
            message.To(new FirstHeaderAs<TOutput>(name, mapping, fallback));
    }
}