using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Repository.Models;

namespace Play.Catalog.Service.Repository
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Item entity);
    }
}