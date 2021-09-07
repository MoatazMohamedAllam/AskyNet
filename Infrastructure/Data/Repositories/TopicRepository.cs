using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AskyContext _context;
        public TopicRepository(AskyContext context)
        {
            _context = context;
        }

        public async Task<Topic> GetByIdAsync(int id) =>
            await _context.Topics.FindAsync(id);

        public async Task<IReadOnlyList<Topic>> ListAllAsync() =>
            await _context.Topics.ToListAsync();

        public async Task<IReadOnlyList<Topic>> GetUserTopics(string userId) =>
            await _context.Topics.Where(t => t.UserId == userId).ToListAsync();
        

        public async Task<Topic> AddAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            return topic;
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            _context.Topics.Attach(topic);
            _context.Entry(topic).State = EntityState.Modified;
            return await Task.FromResult(topic);
        }

        public void RemoveAsync(Topic topic) =>
            _context.Topics.Remove(topic);
    }
}
