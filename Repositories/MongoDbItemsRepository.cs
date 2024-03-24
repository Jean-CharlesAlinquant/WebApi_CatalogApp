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

    public void CreateItem(Item item)
    {
        _itemsCollection.InsertOne(item);
    }

    public void DeleteItem(Guid id)
    {
        var filter = _filterBuilder.Eq(existingItem => existingItem.Id, id);
        _itemsCollection.DeleteOne(filter);
    }

    public Item GetItem(Guid id)
    {
        var filter = _filterBuilder.Eq(item => item.Id, id);
        return _itemsCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Item> GetItems()
    {
        return _itemsCollection.Find(new BsonDocument()).ToList();
    }

    public void UpdateItem(Item item)
    {
        var filter = _filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
        _itemsCollection.ReplaceOne(filter, item);
    }
}