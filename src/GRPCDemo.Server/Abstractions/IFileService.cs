using GRPCDemo.Core;

namespace GRPCDemo.Server.Abstractions
{
    public interface IFileService
    {
        File GetFile(string id);
    }
}
