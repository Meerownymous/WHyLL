namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name as int.
    /// </summary>
    public sealed class FirstHeaderAsInt(string name) : RenderingEnvelope<int>(
        new FirstHeaderAs<int>(name, Convert.ToInt32)
    )
    { }
}

