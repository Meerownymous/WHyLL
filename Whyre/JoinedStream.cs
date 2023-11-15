//using Tonga;
//using Tonga.Enumerable;
//using Tonga.Map;
//using Tonga.Scalar;

//public sealed class JoinedStream : Stream
//{
//    IMap<Stream,long> streams;
//    private readonly Queue<Stream> processing;

//    public JoinedStream(IEnumerable<Stream> streams)
//    {
//        this.streams =
//            AsMap._(
//                Mapped._(
//                    stream => AsPair._(stream, () => stream.Length),
//                    streams
//                )
//            );
//        this.processing = new Queue<Stream>();
//    }

//    public override bool CanRead
//    {
//        get
//        {
//            return true;
//        }
//    }

//    public override int Read(byte[] buffer, int offset, int count)
//    {
//        int totalBytesRead = 0;

//        while (count > 0 && streams.Count > 0)
//        {
//            int bytesRead = streams.Peek().Read(buffer, offset, count);
//            if (bytesRead == 0)
//            {
//                streams.Dequeue().Dispose();
//                continue;
//            }

//            totalBytesRead += bytesRead;
//            offset += bytesRead;
//            count -= bytesRead;
//        }
//        return totalBytesRead;
//    }

//    public override bool CanSeek
//    {
//        get { return false; }
//    }

//    public override bool CanWrite
//    {
//        get { return false; }
//    }

//    public override void Flush()
//    {
//        throw new NotImplementedException();
//    }

//    public override long Length
//    {
//        get
//        {

//        }
//    }

//    public override long Position
//    {
//        get
//        {
//            throw new NotImplementedException();
//        }
//        set
//        {
//            throw new NotImplementedException();
//        }
//    }

//    public override long Seek(long offset, SeekOrigin origin)
//    {
//        throw new NotImplementedException();
//    }

//    public override void SetLength(long value)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Write(byte[] buffer, int offset, int count)
//    {
        
//    }

//    private Stream Current()
//    {
//        if(this.processing.Count == 0)
//        {
//            this.processing
//                .Enqueue(
//                    First._(streams.Pairs()).Value().Key()
//                );
//        }
//        return this.processing.Peek();
//    }
//}