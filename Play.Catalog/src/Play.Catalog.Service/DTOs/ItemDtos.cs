using System;
namespace Play.Catalog.Service.DTOs
{
   public record ReadItemDto (
      Guid Id,
      string Name,
      string Description,
      decimal Price,
      DateTimeOffset CraetedDate
   );

   public record CreateItemDto (
      string Name,
      string Description,
      decimal Price
   );

   public record UpdateItemDto (
      string Name,
      string Description,
      decimal Price
   );
}