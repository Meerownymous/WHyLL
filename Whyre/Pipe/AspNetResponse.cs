//using System.Net.Http.Json;
//using Tonga;
//using Tonga.Map;
//using Whyre.Parts;
//using Whyre.Pipe;

//namespace Whyre.Wire
//{
//	public sealed class AspNetCore : IResponse
//	{
//        private readonly HttpRequestMessage aspRequest;
//        private readonly UriBuilder uriBuild;
//        private readonly Crystal<IPair<string,string>, string> crystallization;

//        private AspNetCore()
//		{
//            this.uriBuild = new UriBuilder();
//            this.aspRequest = new HttpRequestMessage();
//            this.crystallization =
//                Crystal._(Identifier,
//                    When._(new Method(), (method) => this.aspRequest.Method = new HttpMethod(method.Value())),
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
//                    When._(new QueryParam(), (param) =>
//                        this.uriBuild.Query = this.uriBuild.Query += $"{Unprefixed(param)}={param.Value()}"
//                    )
//                );
//        }

//        public IResponse With(string name, string value)
//        {
//            this.crystallization.Invoke(AsPair._(name, value));
//            return this;
//        }

//        public IResponse With(Stream body)
//        {
//            this.aspRequest.Content = new StreamContent(body);
//            return this;
//        }

//        public async Task<T> Render<T>(IRendering<T> target)
//        {
//            HttpResponseMessage aspResponse;
//            target.With(aspResponse)
//        }

//        private static string Identifier(IPair<string, string> prefixedPart)
//        {
//            return prefixedPart.Key().Split(":")[0];
//        }

//        private static IPair<string,string> Unprefixed(IPair<string,string> prefixed)
//        {
//            return AsPair._(prefixed.Key().Split(":")[1], prefixed.Value());
//        }
//    }
//}

