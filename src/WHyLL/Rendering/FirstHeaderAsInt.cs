namespace WHyLL.Rendering
{
    /// <summary>
    /// First header with the given name as int.
    /// </summary>
    public sealed class FirstHeaderAsInt : RenderingEnvelope<int>
    {
        /// <summary>
        /// First header with the given name as int.
        /// </summary>
        public FirstHeaderAsInt(string name) : base(
            new FirstHeaderAs<int>(name, Convert.ToInt32)
        )
        { }
    }
}

