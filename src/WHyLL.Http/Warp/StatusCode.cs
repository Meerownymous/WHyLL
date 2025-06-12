using System.Text.RegularExpressions;
using WHyLL.Http.Warp;
using WHyLL.Warp;

namespace WHyLL.Http.Warp
{
    /// <summary>
    /// Renders the statuscode of a http response.
    /// </summary>
    public sealed class StatusCode() : WarpEnvelope<int>(new PrologueAs<int>(prologue =>
        {
            var parts = prologue.Sequence();
            var line = string.Join(" ", parts);
            if (!Regex.IsMatch(line, "^HTTP\\/\\d\\.\\d\\s\\d{3}\\s.*$"))
                throw new ArgumentException($"'{line}' is not a valid http response.");
            return Convert.ToInt32(parts);
        })
    );
}

namespace WHyLL.Http
{
    public static class StatusCodeSmarts
    {
        public static Task<int> StatusCode(this IMessage msg) => msg.To(new StatusCode());
    }
}