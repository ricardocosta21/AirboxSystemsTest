
using System.Threading.Tasks;

namespace AirboxSystems.API.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
