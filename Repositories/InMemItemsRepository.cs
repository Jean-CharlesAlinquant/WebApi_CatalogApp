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

    public IEnumerable<Item> GetItems()
    {
        return _items;
    }

    public Item GetItem(Guid id)
    {
        return _items.FirstOrDefault(item => item.Id == id)!;
    }

    public void CreateItem(Item item)
    {
        _items.Add(item);
    }

    public void UpdateItem(Item item)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
        _items[index] = item;
    }

    public void DeleteItem(Guid id)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == id);
        _items.RemoveAt(index);
    }
}
