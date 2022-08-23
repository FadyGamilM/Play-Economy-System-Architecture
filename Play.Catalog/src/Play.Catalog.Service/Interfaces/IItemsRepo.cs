using Play.Catalog.Service.Models;
namespace Play.Catalog.Service.Interfaces
{
   public interface IItemsRepo
   {
      Task<IEnumerable<Item>> GetItems();

      Task<Item> GetItemById(Guid ItemID);

      Task CreateItem(Item item);

      Task UpdateItem(Guid itemID, Item item);

      Task DeleteItem(Guid itemID);
   } 
}