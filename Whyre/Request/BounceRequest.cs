//using Tonga;
//using Tonga.Collection;
//using Tonga.Map;

//namespace Whyre.Request
//{
//    /// <summary>
//    /// Request which bounces back by just rendering itself.
//    /// </summary>
//	public sealed class BounceRequest : IMessage
//	{
//        private readonly IMap<string, ICollection<string>> before;
//        private readonly Stream body;

//        /// <summary>
//        /// Request which bounces back by just rendering itself.
//        /// </summary>
//        public BounceRequest() : this(
//            Tonga.Map.Empty._<string,ICollection<string>>(),
//            new MemoryStream()
//        )
//        { }

//        /// <summary>
//        /// Request which bounces back by just rendering itself.
//        /// </summary>
//        private BounceRequest(IMap<string, ICollection<string>> parts, Stream body)
//		{
//            this.before = parts;
//            this.body = body;
//        }

//        public async Task<T> Render<T>(IRendering<T> rendering)
//        {
//            foreach (var part in this.before.Pairs())
//                foreach (var value in part.Value())
//                    rendering = rendering.Refine(part.Key(), value);
//            return await Task.FromResult(rendering.Render(this.body));
//        }

//        public IMessage Refined(IPair<string, string> header)
//        {
//            IMap<string,ICollection<string>> after;
//            if (!this.before.Keys().Contains(header.Key()))
//                after =
//                    this.before
//                        .With(
//                            AsPair._(header.Key(), AsCollection._(header.Value()))
//                        );
//            else
//                after =
//                    this.before
//                        .With(
//                            AsPair._(
//                                header.Key(),
//                                Joined._(
//                                    this.before[header.Key()],
//                                    Tonga.Enumerable.Single._(header.Value())
//                                )
//                            )
//                        );
//            return new BounceRequest(after, this.body);
//        }

//        public IMessage Refine(Stream body)
//        {
//            return new BounceRequest(this.before, body);
//        }

//        public IMessage Refined(IMessageInput input)
//        {
//            return input.WriteTo(this);
//        }
//    }
//}

