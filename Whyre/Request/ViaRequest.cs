//using Tonga;
//using Tonga.Enumerable;
//using Whyre.Parts;

//namespace Whyre
//{
//    /// <summary>
//    /// Simple HTTP Request.
//    /// </summary>
//    public sealed class ViaRequest : IMessage
//    {
//        private readonly IEnumerable<IMessageInput> parts;
//        private readonly Stream body;

//        public ViaRequest(IEnumerable<IMessageInput> parts, Stream body)
//        {
//            this.parts = parts;
//            this.body = body;
//        }

//        public IMessage Refined(IPair<string, string> header)
//        {
//            return
//                new ViaRequest(
//                    Joined._(parts, AsRequestInput._(header)),
//                    this.body
//                );
//        }

//        public IMessage Refined(IMessageInput input)
//        {
//            return
//                new ViaRequest(
//                    Joined._(this.parts, input),
//                    this.body
//                );
//        }

//        public IMessage Refine(Stream body)
//        {
//            return new ViaRequest(this.parts, this.body);
//        }

//        public IMessage As(IMessage request)
//        {
//            var after = request.Refine(this.body);
//            foreach(var part in this.parts)
//            {
//                after = after.Refined(part);
//            }
//            return after;
//        }

//        public async Task<T> Render<T>(IRendering<T> rendering)
//        {
//            return
//                Task.FromResult(() =>
                    
//                );
            
//        }
//    }
//}

