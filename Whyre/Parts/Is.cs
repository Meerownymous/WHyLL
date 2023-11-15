using System;
using Tonga;

namespace Whyre.Parts
{
	public sealed class Is : IScalar<bool>
	{
        private readonly IPair<string, string> part;
        private readonly string candidate;

        public Is(IPair<string,string> part, string candidate)
        {
            this.part = part;
            this.candidate = candidate;
        }

        public bool Value()
        {
            var index = this.part.Key().IndexOf(":");
            index = index > -1 ? index : this.part.Key().Length;
            var result = false;
            if(this.candidate.Length >= index)
                result =
                    this.part.Key().Substring(0, index)
                    ==
                    this.candidate.Substring(0, index);

            return result;
        }

        public static Is _(IPair<string, string> part, string candidate) =>
            new Is(part, candidate);
    }
}

