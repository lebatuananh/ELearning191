using System.Threading.Tasks;

namespace Shared.Initialization
{
    public interface IInitializationStage
    {
        int Order { get; }
        Task ExecuteAsync();
    }
}
