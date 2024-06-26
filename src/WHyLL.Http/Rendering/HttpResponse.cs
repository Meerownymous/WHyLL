﻿using Tonga;
using Tonga.Enumerable;
using Tonga.Text;
using WHyLL.Message;

namespace WHyLL.Rendering.Http
{
    /// <summary>
    /// Renders a response message using Asp.Net Core HttpClient.
    /// </summary>
    public sealed class HttpResponse : IRendering<IMessage>
    {
        private readonly HttpRequestMessage message;
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> convert;

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpResponse(HttpClient client) : this(new HttpRequestMessage(),
            message => client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead)
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpResponse(HttpResponseMessage result) : this(
            new HttpRequestMessage(),
            message => Task.FromResult(result)
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpResponse(Func<HttpRequestMessage, Task<HttpResponseMessage>> convert) : this(
            new HttpRequestMessage(),
            convert.Invoke
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpResponse(HttpRequestMessage message, Func<HttpRequestMessage, Task<HttpResponseMessage>> convert)
        {
            this.message = message;
            this.convert = convert;
        }

        public IRendering<IMessage> Refine(string requestLine)
        {
            var pieces = RequestLineParts(requestLine);
            this.message.Method = new HttpMethod(pieces[0]);
            this.message.RequestUri = new Uri(pieces[1]);
            this.message.Version =
                new Version(
                    new TrimmedLeft(
                        new TrimmedRight(pieces[2], "\r\n"),
                        "HTTP/"
                    ).AsString()
                );
            return new HttpResponse(message, convert);
        }

        public IRendering<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<IMessage> Refine(params IPair<string, string>[] parts)
        {
            foreach(var part in parts)
                this.message.Headers.TryAddWithoutValidation(part.Key(), part.Value());
            return new HttpResponse(this.message, this.convert);
        }

        public IRendering<IMessage> Refine(Stream body)
        {
            this.message.Content = new StreamContent(body);
            return new HttpResponse(this.message, this.convert);
        }

        public async Task<IMessage> Render()
        {
            var aspResponse = await this.convert(this.message);
            return
                new SimpleMessage(
                    $"HTTP/{aspResponse.Version} {(int)aspResponse.StatusCode} {aspResponse.ReasonPhrase}\r\n",
                    Tonga.Enumerable.Joined._(
                        Mapped._(
                            header => 
                            Mapped._(
                                value => Tonga.Map.AsPair._(header.Key, value),
                                header.Value
                            ),
                            aspResponse.Headers
                        )
                    ),
                    aspResponse.Content.ReadAsStream()
                );
        }

        private static string[] RequestLineParts(string requestLine)
        {
            return requestLine.Split(" ");
        }
    }
}