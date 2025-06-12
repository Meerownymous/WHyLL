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
public sealed class JsonBodyAs<TResult>(IOptional<JSchema> schema, Func<JObject, ValueTask<TResult>> warp)
    : WarpEnvelope<TResult>(
        new BodyAs<TResult>(async body =>
        {
            var json =
                JObject.Parse(
                    await new StreamReader(body, Encoding.UTF8)
                        .ReadToEndAsync()
                );
            
            if(body.CanSeek)
                body.Seek(0, SeekOrigin.Begin);
            
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
{
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonBodyAs(Func<JObject, ValueTask<TResult>> warp) : this(
        new OptEmpty<JSchema>(), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonBodyAs(Func<JObject, TResult> warp) : this(
        new OptEmpty<JSchema>(), body => ValueTask.FromResult(warp(body))
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonBodyAs(JSchema schema, Func<JObject, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonBodyAs(Func<JSchema> schema, Func<JObject, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema), warp
    )
    { }
    
    /// <summary>
    /// Incoming body parsed as json, optionally validated and warped by
    /// a given function.
    /// </summary>
    public JsonBodyAs(IScalar<JSchema> schema, Func<JObject, ValueTask<TResult>> warp) : this(
        new OptFull<JSchema>(schema.Value), 
        warp
    )
    { }
}