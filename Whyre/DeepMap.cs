using System;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;

namespace Whyre
{
    /// <summary>
    /// Maps a deeper level of a key to a value.
    /// </summary>
    public sealed class DeepMap<Surface, Deep, Value> : IMap<Surface, Value>
    {
        private readonly Func<Surface, Deep> digDown;
        private readonly AsMap<Deep, Value> deep;

        /// <summary>
        /// Maps a deeper level of a key to a value.
        /// </summary>
        public DeepMap(Func<Surface, Deep> digDown, IMap<Surface, Value> origin)
        {
            this.digDown = digDown;
            this.deep =
                AsMap._(
                    Mapped._(
                        pair => AsPair._(digDown(pair.Key()), pair.Value()),
                        origin.Pairs()
                    )
                );

        }

        public Value this[Surface key]
        {
            get
            {
                return this.deep[this.digDown(key)];
            }
        }

        public ICollection<Surface> Keys()
        {
            throw new NotImplementedException();
        }

        public Func<Value> Lazy(Surface key)
        {
            return () => this[key];
        }

        public IEnumerable<IPair<Surface, Value>> Pairs()
        {
            throw new NotImplementedException();
        }

        public IMap<Surface, Value> With(IPair<Surface, Value> pair)
        {
            throw new NotImplementedException();
        }
    }

    public static class DeepMap
    {
        public static DeepMap<Surface, Deep, Value> _<Surface, Deep, Value>(
            Func<Surface, Deep> digDown, IMap<Surface, Value> origin
        ) => new DeepMap<Surface, Deep, Value>(digDown, origin);
    }

}

