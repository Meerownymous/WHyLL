using Tonga.Text;

namespace Tonga;

public static partial class TextSmarts
{
    public static async Task<IText> AsText(this Task<Stream> stream) => 
        (await stream).AsText();
    
    public static async Task<string> Str(this Task<IText> text) => 
        (await text).Str();
}