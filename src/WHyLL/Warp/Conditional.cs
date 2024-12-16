namespace WHyLL.Warp;

/// <summary>
/// Warp depending on a condition.
/// </summary>
public sealed class Conditional<TOutcome>(
    Func<IMessage, Task<bool>> condition,
    IWarp<TOutcome> yes,
    IWarp<TOutcome> no)
    : WarpEnvelope<TOutcome>(
        new MessageAs<TOutcome>(async msg =>
            await condition(msg)
                ? await msg.To(yes)
                : await msg.To(no)
        )
    )
{
    /// <summary>
    /// Warp depending on a condition.
    /// </summary>
    public Conditional(
        Func<IMessage, bool> condition,
        IWarp<TOutcome> yes,
        IWarp<TOutcome> no
    ) : this(message => Task.FromResult(condition(message)), 
        yes, 
        no
    ){ }
}