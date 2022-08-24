using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Models;
namespace Play.Catalog.Service
{
   public static class Extensions 
   {
      public static ReadItemDto AsDto(this Item item)
      {
         return new ReadItemDto(
            item.Id,
            item.Name,
            item.Description,
            item.Price,
            item.CreatedDate
         );
      }
   }
}