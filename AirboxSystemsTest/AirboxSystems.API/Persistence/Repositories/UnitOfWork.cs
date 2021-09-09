using System.Threading.Tasks;
using AirboxSystems.API.Persistence.Repositories;
using AirboxSystems.API.Persistence.Contexts;

namespace supermarketapi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}