using System;
using Tonga;
using Tonga.Enumerable;

namespace Whyre.Parts
{
	public sealed class AsRequestInput : IRequestInput
	{
        private readonly IEnumerable<IPair<string, string>> parts;

        public AsRequestInput(IPair<string, string>[] parts) : this(
            AsEnumerable._(parts)
        )
        { }

        public AsRequestInput(IEnumerable<IPair<string,string>> parts)
		{
            this.parts = parts;
        }

        public IRequest WriteTo(IRequest request)
        {
            foreach(var part in this.parts)
            {
                request = request.Refined(part);
            }
            return request;
        }

        public static IRequestInput _(params IPair<string, string>[] parts) =>
            new AsRequestInput(parts);

        public static IRequestInput _(IEnumerable<IPair<string, string>> parts) =>
            new AsRequestInput(parts);
    }
}

