using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tonga.IO;
using WHyLL;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.MessageInput
{
    /// <summary>
    /// Json body input for a <see cref="IMessage"/>.
    /// </summary>
    public sealed class JsonBodyInput(Func<JObject> json, Formatting formatting = Formatting.Indented) : IMessageInput
    {
        public JsonBodyInput(JObject json, Formatting formatting = Formatting.Indented) : this (() => json)
        { }
        
        public IMessage WriteTo(IMessage message) =>
            message.WithBody(
                new AsStream(
                    JsonConvert.SerializeObject(json, formatting)
                )    
            );
    }
}

namespace BodyInputSmarts
{
    public static class JsonBodyInputSmarts
    {
        /// Message with json body.
        public static IMessage Body(this IMessage msg, JObject body, Formatting formatting = Formatting.Indented) => 
            new MessageWithInputs(
                msg,
                new JsonBodyInput(body, formatting)
            );
    }
}