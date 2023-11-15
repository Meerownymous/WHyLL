//using System;
//using Tonga;

//namespace Whyre.Response
//{
//	public sealed class SimpleResponse : IResponse
//	{
//        private readonly Stream body;

//        public SimpleResponse() : this(
//            new MemoryStream()
//        )
//        { }

//        private SimpleResponse(Stream body)
//		{
//            this.body = body;
//        }

//        public IResponse With(string name, string value)
//        {
//            return this;
//        }

//        public IResponse With(Stream body)
//        {
//            return new SimpleResponse(body);
//        }

//        public Task<T> Render<T>(IRendering<T> rendering)
//        {
//            return rendering.Render();
//        }
//    }
//}

