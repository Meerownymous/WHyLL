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
                new Base64Decoded(
                    await new StreamReader(body, Encoding.UTF8)
                        .ReadToEndAsync()
                ).AsString()
            );
            if (schema.Has() && !json.IsValid(schema.Value(), out IList<ValidationError> errors))
            {
                throw new ArgumentException(
                    new Paragraph(
                        "Input json is not valid:",
                        Mapped._(
                            error => error.Message,
                            errors
                        ).ToArray(),
                        "json:",
                        json.ToString()
                    ).AsString()
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