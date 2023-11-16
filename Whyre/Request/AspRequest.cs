//using System.Net.Http.Json;
//using Tonga;
//using Tonga.Bytes;
//using Tonga.Map;
//using Whyre.Parts;

//namespace Whyre.Wire
//{
//	public sealed class AspRequest : IMessage
//	{
//        private readonly HttpRequestMessage aspRequest;
//        private readonly UriBuilder uriBuild;
//        private readonly Crystal crystallization;

//        private AspRequest()
//		{
//            this.uriBuild = new UriBuilder();
//            this.aspRequest = new HttpRequestMessage();
//            this.crystallization =
//                Crystal._(
//                    //When._(new Method(), (method) => this.aspRequest.Method = new HttpMethod(method.Value())),
//                    When._(new Parts.Path(), (path) => this.uriBuild.Path = path.Value()),
//                    When._(new Host(), (host) => this.uriBuild.Host = host.Value()),
//                    When._(new Port(), (port) => this.uriBuild.Port = int.Parse(port.Value())),
//                    When._(new Scheme(), (scheme) => this.uriBuild.Scheme = scheme.Value()),
//                    When._(new Header(), (header) =>
//                        this.aspRequest
//                            .Headers
//                            .TryAddWithoutValidation(
//                                Unprefixed(header).Key(),
//                                header.Value()
//                            )
//                    ),
//                    When._(new Query(), (param) =>
//                        this.uriBuild.Query = this.uriBuild.Query += $"{Unprefixed(param)}={param.Value()}"
//                    )
//                );
//        }

//        public IMessage With(IPair<string, string> part)
//        {
//            this.crystallization.Invoke(part);
//            return this;
//        }

//        public IMessage WithBody(Stream body)
//        {
//            this.aspRequest.Content = new StreamContent(body);
//            return this;
//        }

//        public async Task<T> Render<T>(IRendering<T> output)
//        {
//            throw new Exception();
//            //return
//            //    (await Task.Run(() => output.Refine("status", "done")))
//            //        .Render(
//            //            new MemoryStream(
//            //                new AsBytes("I am a body").Bytes()
//            //            )
//            //        );
//        }

//        private static string ByIdentifier(IPair<string, string> prefixedPart)
//        {
//            return prefixedPart.Key().Split(":")[0];
//        }

//        private static IPair<string,string> Unprefixed(IPair<string,string> prefixed)
//        {
//            return AsPair._(prefixed.Key().Split(":")[1], prefixed.Value());
//        }
//    }
//}

