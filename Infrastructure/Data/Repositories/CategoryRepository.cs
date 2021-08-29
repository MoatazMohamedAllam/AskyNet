using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AskyContext _context;
        public CategoryRepository(AskyContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByIdAsync(int id) =>
             await _context.Categories.FindAsync(id);

        public async Task<IReadOnlyList<Category>> ListAllAsync() =>
            await _context.Categories.ToListAsync();


        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            return await Task.FromResult(category);
        }

        public void RemoveAsync(Category category) =>
            _context.Categories.Remove(category);
    }
}
