using System;
using Tonga;

namespace Whyre.Parts
{
    /// <summary>
    /// Wrap for a request head.
    /// </summary>
	public abstract class PartWrap : IPair<string,string>
	{
        private readonly string name;
        private readonly Func<string> value;
        private readonly bool isLazy;

        /// <summary>
        /// Wrap for a request head.
        /// </summary>
        public PartWrap(string name, Func<string> value) : this(name, value, true)
        { }

        /// <summary>
        /// Wrap for a request head.
        /// </summary>
        public PartWrap(string name, string value) : this(name, () => value, false)
        { }

        /// <summary>
        /// Wrap for a request head.
        /// </summary>
        public PartWrap(string name, IMap<string,string> request) : this(
            name, () => request.Keys().Contains(name) ? request[name] : String.Empty
        )
        { }

        /// <summary>
        /// Wrap for a request head.
        /// </summary>
        public PartWrap(string name, Func<string> value, bool isLazy)
		{
            this.name = name;
            this.value = value;
            this.isLazy = isLazy;
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }

        public string Key()
        {
            return this.name;
        }

        public string Value()
        {
            return this.value();
        }
    }
}

