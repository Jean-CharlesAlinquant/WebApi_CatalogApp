using Catalog.Entities;

namespace Catalog.Repositories;

public interface IItemsRepository
{
    Task<IEnumerable<Item>> GetItemsAsynch();
    Task<Item> GetItemAsynch(Guid id);
    Task CreateItemAsynch(Item item);
    Task UpdateItemAsynch(Item item);
    Task DeleteItemAsynch(Guid id);
}
