using System.Threading.Tasks;
using GRPCDemo.Core;

namespace GRPCDemo.Client
{
    public interface IDemoClient
    {
        Task<string> Echo(string Message);
        Task<File> GetFile(string id);
    }
}