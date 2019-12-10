using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace GRPCDemo.Core
{
    public partial class File
    {
        #region Properties

        public byte[] ByteContent {
            get => Content.ToByteArray();
            set => Content = ByteString.CopyFrom(value);
        }

        #endregion

        #region Constructors

        private File(string iD, string name, string extension)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Extension = extension ?? throw new ArgumentNullException(nameof(extension));
        }

        public File(string iD, string name, string extension, ByteString content) : this(iD, name, extension)
            => Content = content ?? throw new ArgumentNullException(nameof(content));

        public File(string iD, string name, string extension, byte[] content) : this(iD, name, extension)
            => ByteContent = content ?? throw new ArgumentNullException(nameof(content));

        public static async Task<File> FromChunkStream(IAsyncStreamReader<FileChunk> Chunks, CancellationToken cancellationToken = default)
        {
            var _chunks = new List<FileChunk>();

            while (await Chunks.MoveNext())
                _chunks.Add(Chunks.Current);

            if (!_chunks.Any()) throw new InvalidOperationException("Stream is empty");

            var firstChunk = _chunks.First();
            return new File(firstChunk.ID, firstChunk.Name, firstChunk.Extension, _chunks.SelectMany(c => c.Chunk).ToArray());
        }

        #endregion

        #region Methods

        public IEnumerable<FileChunk> ToChunks(int chunkSize)
        {
            long count = Content.LongCount();
            long chunkcount = count / chunkSize;
            if (count % chunkSize > 0) chunkcount += 1;

            for (int i = 0; i < chunkcount; i++)
                yield return new FileChunk(this.ID, this.Name, this.Extension, ByteString.CopyFrom(Content.Skip(i * chunkSize).Take(chunkSize).ToArray()));
        }

        #endregion

    }

    public partial class FileChunk
    {
        public FileChunk(string iD, string name, string extension, ByteString chunk)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Extension = extension ?? throw new ArgumentNullException(nameof(extension));
            Chunk = chunk ?? throw new ArgumentNullException(nameof(chunk));
        }
    }
}
