using System.Text.RegularExpressions;
using WHyLL.Rendering;

namespace WHyLL.Http.Rendering
{
    /// <summary>
    /// Renders the statuscode of a http response.
    /// </summary>
    public sealed class StatusCode : RenderingEnvelope<int>
    {
        /// <summary>
        /// Renders the statuscode of a http response.
        /// </summary>
        public StatusCode(): base(new FirstLineAs<int>((line) =>
            {
                if (!Regex.IsMatch(line, "^HTTP\\/\\d\\.\\d\\s\\d{3}\\s.*$"))
                    throw new ArgumentException($"'{line}' is not a valid http response.");
                return Convert.ToInt32(line.Split(" ")[1]);
            })
        )
        { }
    }
}

