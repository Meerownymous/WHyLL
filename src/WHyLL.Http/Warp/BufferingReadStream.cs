namespace WHyLL.Http.Warp;

/// <summary>
/// Stream that buffers what it has already read, and delivers subsequent reads from its buffer.
/// </summary>
public class BufferingReadStream : Stream
{
    private readonly Stream innerStream;
    private readonly List<byte> buffer = new();
    private long position;

    /// <summary>
    /// Stream that buffers what it has already read, and delivers subsequent reads from its buffer.
    /// </summary>
    public BufferingReadStream(Stream innerStream)
    {
        this.innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
    }

    public override bool CanRead => innerStream.CanRead;
    public override bool CanSeek => true;
    public override bool CanWrite => false;
    public override long Length => innerStream.Length;

    public override long Position
    {
        get => position;
        set
        {
            if (value < 0 || value > buffer.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(Position), "Position is out of range.");
            }
            position = value;
        }
    }

    public override void Flush()
    {
        innerStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        if (offset < 0 || count < 0 || (offset + count) > buffer.Length)
            throw new ArgumentOutOfRangeException("Invalid offset or count.");

        int bytesRead = 0;

        // If position is within the buffer, read from the buffer first
        if (position < this.buffer.Count)
        {
            int bytesFromBuffer = Math.Min(count, this.buffer.Count - (int)position);
            this.buffer.CopyTo((int)position, buffer, offset, bytesFromBuffer);
            position += bytesFromBuffer;
            bytesRead += bytesFromBuffer;
            offset += bytesFromBuffer;
            count -= bytesFromBuffer;
        }

        // If more data is needed, read from the inner stream
        if (count > 0)
        {
            byte[] tempBuffer = new byte[count];
            int innerStreamBytesRead = innerStream.Read(tempBuffer, 0, count);
            if (innerStreamBytesRead > 0)
            {
                // Add read bytes to the buffer
                this.buffer.AddRange(tempBuffer[..innerStreamBytesRead]);
                Array.Copy(tempBuffer, 0, buffer, offset, innerStreamBytesRead);
                position += innerStreamBytesRead;
                bytesRead += innerStreamBytesRead;
            }
        }

        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        long newPosition;
        switch (origin)
        {
            case SeekOrigin.Begin:
                newPosition = offset;
                break;
            case SeekOrigin.Current:
                newPosition = position + offset;
                break;
            case SeekOrigin.End:
                newPosition = buffer.Count + offset;
                break;
            default:
                throw new ArgumentException("Invalid SeekOrigin", nameof(origin));
        }

        if (newPosition < 0 || newPosition > buffer.Count)
            throw new ArgumentOutOfRangeException(nameof(offset), "Cannot seek outside of the buffer range.");

        position = newPosition;
        return position;
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException("SetLength is not supported.");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException("Write is not supported.");
    }
}