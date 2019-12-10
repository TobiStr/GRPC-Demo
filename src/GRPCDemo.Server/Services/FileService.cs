using GRPCDemo.Core;
using GRPCDemo.Server.Abstractions;

namespace GRPCDemo.Server.Services
{
    public class FileService : IFileService
    {
        public File GetFile(string id)
            => new File(id, "Test", ".jpg", 
                System.IO.File.ReadAllBytes($@"{System.IO.Path.GetDirectoryName(typeof(FileService).Assembly.Location)}\Resources\Test.jpg"));
    }
}
