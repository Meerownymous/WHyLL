using WHyLL;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Render the prologue of a request (request-line) or response (status-line)
    /// </summary>
    public sealed class Prologue() : WarpEnvelope<string>(
        new PrologueAs<string>(line => string.Join(" ", line.Sequence()))
    );

}

    public static partial class WarpSmarts
    {
        /// <summary>
        /// Prologue of the message.
        /// </summary>
        public static Task<string> Prologue(this IMessage message) => 
            message.To(new Prologue());
    }