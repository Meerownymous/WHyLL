using WHyLL;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Render the first line of a message as output type.
    /// </summary>
    public sealed class PrologueAs<Output> : WarpEnvelope<Output>
    {
        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        public PrologueAs(Func<IPrologue, Output> render) : base(
            new PiecesAs<Output>((prologue, _, _) => Task.FromResult(render(prologue)))
        )
        { }

        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        public PrologueAs(Func<IPrologue, Task<Output>> render) : base(
            new PiecesAs<Output>((prologue, _, _) => render(prologue))
        )
        { }
    }
}
public static partial class WarpSmarts
{
    /// <summary>
    /// Firstline of the message.
    /// </summary>
    public static async Task<TOutput> PrologueAs<TOutput>(
        this IMessage message, 
        Func<IPrologue, TOutput> render
    ) => 
        await message.To(new PrologueAs<TOutput>(render));
}