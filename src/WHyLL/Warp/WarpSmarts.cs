namespace WHyLL.Warp
{
    public static class WarpSmarts
    {
        public static async Task<T> To<T>(this Task<IMessage> responseTask, IWarp<T> warp)
        {
            return await (await responseTask).To(warp);
        }
    }
}