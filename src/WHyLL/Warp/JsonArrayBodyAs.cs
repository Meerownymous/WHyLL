using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Tonga;
using Tonga.Enumerable;
using Tonga.Optional;
using Tonga.Text;

namespace WHyLL.Warp;

/// <summary>
/// Incoming body parsed as json, optionally validated and warped by
/// a given function.
/// </summary>
public sealed class JsonArrayBodyAs<TResult> : WarpEnvelope<TResult>
{
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonArrayBodyAs(IOptional<JSchema> schema, Func<JArray, ValueTask<TResult>> warp) : base(
        new BodyAs<TResult>(async body =>
        {
            var json = 
                JArray.Parse(
                    new Base64Decoded(
                        await new StreamReader(body, Encoding.UTF8)
                            .ReadToEndAsync()
                    ).Str()
                );
            if (schema.Has() && !json.IsValid(schema.Value(), out IList<ValidationError> errors))
            {
                throw new ArgumentException(
                    "Input json is not valid:\r\n"
                    + string.Join("\r\n", errors.AsMapped(error => error.Message))
                    + "json:",
                        json.ToString()
                );
            }
            return await warp(json);
        })
    )
    { }

    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonArrayBodyAs(Func<JArray, ValueTask<TResult>> warp) : this(
        new OptEmpty<JSchema>(), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonArrayBodyAs(JSchema schema, Func<JArray, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonArrayBodyAs(Func<JSchema> schema, Func<JArray, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonArrayBodyAs(IScalar<JSchema> schema, Func<JArray, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema.Value), 
        warp
    )
    { }
}