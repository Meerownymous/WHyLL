namespace WHyLL.Warp
{
    public static class WarpSmarts
    {
        /// <summary>
        /// Fluent async chaining.
        /// </summary>
        public static async Task<T> To<T>(this Task<IMessage> responseTask, IWarp<T> warp) =>
            await (await responseTask).To(warp);
    }
}