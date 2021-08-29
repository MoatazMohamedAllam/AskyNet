using Core;
using Core.Repositories;
using Infrastructure.Data.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AskyContext _context;
        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(AskyContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
        }

        public async Task<int> Complete() =>
             await _context.SaveChangesAsync();

        public void Dispose() =>
            _context.Dispose();
    }
}
