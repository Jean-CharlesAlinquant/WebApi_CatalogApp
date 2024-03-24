using Catalog.Controllers;
using Catalog.Entities;

namespace Catalog.Repositories;

public class InMemItemsRepository : IItemsRepository
{
    private readonly List<Item> _items = new() // C# 9.0
    {
        new Item {Id= Guid.NewGuid(), Name="Potion", Price = 9, CreateDate=DateTimeOffset.UtcNow},
        new Item {Id= Guid.NewGuid(), Name="Iron Sword", Price = 20, CreateDate=DateTimeOffset.UtcNow},
        new Item {Id= Guid.NewGuid(), Name="Bronze Shield", Price = 18, CreateDate=DateTimeOffset.UtcNow}
    };

    public async Task<IEnumerable<Item>> GetItemsAsynch()
    {
        return await Task.FromResult(_items);
    }

    public async Task<Item> GetItemAsynch(Guid id)
    {
        var item = _items.FirstOrDefault(item => item.Id == id)!;
        return await Task.FromResult(item);
    }

    public async Task CreateItemAsynch(Item item)
    {
        _items.Add(item);
        await Task.CompletedTask;
    }

    public async Task UpdateItemAsynch(Item item)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
        _items[index] = item;
        await Task.CompletedTask;
    }

    public async Task DeleteItemAsynch(Guid id)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == id);
        _items.RemoveAt(index);
        await Task.CompletedTask;
    }
}
