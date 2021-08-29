using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITopicRepository
    {
        Task<Topic> GetByIdAsync(int id);
        Task<IReadOnlyList<Topic>> ListAllAsync();
        Task<Topic> AddAsync(Topic topic);
        Task<Topic> UpdateAsync(Topic topic);
        void RemoveAsync(Topic topic);
    }
}
