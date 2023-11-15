//using System;
//using Tonga;
//using Tonga.Text;

//namespace Whyre
//{
//	public sealed class Required<Key,Value> : IMap<Key,Value>
//	{
//        private readonly List<Key> required;
//        private readonly List<IPair<Key, Value>> content;

//        public Required(IEnumerable<Key> requiredKeys)
//		{
//            this.required = new List<string>(requiredKeys);
//        }

//        public Value this[Key key]
//        {
//            get
//            {
//                HttpClient client = new HttpClient();
//                HttpMessageHandler handler;
                
                
//                if (!IsComplete(this.required))
//                    throw new InvalidOperationException($"Required content missing: {Joined._(",", this.required)}");
//                return 
//            }
//        }

//        public ICollection<string> Keys()
//        {
//            throw new NotImplementedException();
//        }

//        public Func<string> Lazy(string key)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPair<string, string>> Pairs()
//        {
//            throw new NotImplementedException();
//        }

//        public IMap<string, string> With(IPair<string, string> pair)
//        {
//            throw new NotImplementedException();
//        }

//        private static bool IsComplete(IList<string> required)
//        {
//            return required.Count == 0;
//        }
//    }
//}

