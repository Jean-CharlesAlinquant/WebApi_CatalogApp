using System.Runtime.CompilerServices;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories;

public class MongoDbItemsRepository : IItemsRepository
{
    private readonly IMongoCollection<Item> _itemsCollection;
    private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;
    private const string DB_NAME = "Catalog";
    private const string COLLECTION_NAME = "Items";
    public MongoDbItemsRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(DB_NAME);
        _itemsCollection = database.GetCollection<Item>(COLLECTION_NAME);
    }

    public async Task CreateItemAsynch(Item item)
    {
        await _itemsCollection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsynch(Guid id)
    {
        var filter = _filterBuilder.Eq(existingItem => existingItem.Id, id);
        await _itemsCollection.DeleteOneAsync(filter);
    }

    public async Task<Item> GetItemAsynch(Guid id)
    {
        var filter = _filterBuilder.Eq(item => item.Id, id);
        return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsynch()
    {
        return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateItemAsynch(Item item)
    {
        var filter = _filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
        await _itemsCollection.ReplaceOneAsync(filter, item);
    }
}