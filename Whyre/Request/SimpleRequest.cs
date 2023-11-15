using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;

namespace Whyre
{
    /// <summary>
    /// Simple HTTP Request.
    /// </summary>
    public sealed class SimpleRequest : IRequest
    {
        private readonly Func<IRequest> initial;

        public SimpleRequest(IRequest via, params IPair<string, string>[] parts) : this(
            via,
            Mapped._(
                part => AsRequestInput._(part),
                parts
            )
        )
        { }

        public SimpleRequest(IRequest via, params IRequestInput[] parts) : this(via, AsEnumerable._(parts))
        { }

        public SimpleRequest(IRequest via, IPair<string,string> part, IEnumerable<IRequestInput> parts) : this(
            via,
            Joined._(
                Tonga.Enumerable.Single._(
                    AsRequestInput._(part)
                ),
                parts
            )
        )
        { }

        public SimpleRequest(IRequest via, IEnumerable<IRequestInput> parts)
        {
            this.initial = () =>
                {
                    foreach (var input in parts)
                    {
                        via = input.WriteTo(via);
                    }
                    return via;
                };
        }

        public IRequest Refined(IPair<string, string> header)
        {
            return
                this.initial()
                    .Refined(header);
        }

        public IRequest Refined(IRequestInput input)
        {
            return input.WriteTo(this.initial());
        }

        public IRequest Refine(Stream body)
        {
            return
                this.initial()
                    .Refine(body);
        }

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            return
                await
                    this.initial()
                        .Render(rendering);
        }
    }
}

