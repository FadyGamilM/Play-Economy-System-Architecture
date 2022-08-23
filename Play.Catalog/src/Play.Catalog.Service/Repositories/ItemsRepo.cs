using Play.Catalog.Service.Models;
using Play.Catalog.Service.Interfaces;
using MongoDB.Driver;

namespace Play.Catalog.Service.Repositories
{
   public class ItemsRepo : IItemsRepo
   {
      // the DI pattern and mongodb setup
      private const string databaseName = "catalogServiceDB";
      private const string collectionName = "Items";
      private readonly IMongoCollection<Item> itemsCollection;
      public ItemsRepo(IMongoClient mongoClient)
      {
         // get database instance
         var database = mongoClient.GetDatabase(databaseName);
         // get collection instance
         itemsCollection = database.GetCollection<Item>(collectionName);
      }

      // get all items
      public async Task CreateItem(Item item)
      {
         await this.itemsCollection.InsertOneAsync(item);
      }

      public async Task DeleteItem(Guid itemID)
      {
         await this.itemsCollection.FindOneAndDeleteAsync(item => item.Id == itemID);
      }

      public async Task<Item> GetItemById(Guid ItemID)
      {
         var item = await this.itemsCollection.Find(item => item.Id == ItemID).FirstOrDefaultAsync();
         return item;
      }

      public async Task<IEnumerable<Item>> GetItems()
      {
         var items = await this.itemsCollection.Find(item => true).ToListAsync();
         return items;
      }

      public async Task UpdateItem(Guid itemID, Item item)
      {
         await this.itemsCollection.FindOneAndReplaceAsync(item => item.Id == itemID, item);
      }
   }
}