using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;
namespace Play.Catalog.Service.Controllers
{
   [ApiController]
   [Route("api/items")]
   public class ItemsController : ControllerBase
   {
      private static readonly List<ReadItemDto> items = new ()
      {
         new ReadItemDto(Guid.NewGuid(), "Master Sword", "The Legend Of Zenda", 600, DateTimeOffset.UtcNow),
         new ReadItemDto(Guid.NewGuid(), "Golden Gun", "GoldenEye 007", 700, DateTimeOffset.UtcNow),
         new ReadItemDto(Guid.NewGuid(), "Portan Gun", "Portal", 360, DateTimeOffset.UtcNow)
      };
      public ItemsController()
      {
         
      }

      // --> URL: api/items
      // --> Desc: Get all items from DB
      // --> response: status code 200 and list of items
      [HttpGet("")]
      public ActionResult GetItems ()
      {
         return Ok(items);
      }

      // --> URL: api/items/{Id}
      // --> Desc: Get one item by its id
      // --> response: status code 200 and returns an item
      [HttpGet("{Id}", Name="GetItemsById")]
      public ActionResult GetItemById([FromRoute]Guid Id)
      {
         var item = items.Where(item => item.Id == Id).FirstOrDefault();
         if (item == null){
            return NotFound();
         }
         return Ok(item);
      }
      // --> URL: api/items
      // --> Desc: create a new item
      // --> response: status code 201 and returns the created item
      [HttpPost]
      public ActionResult CreateItem([FromBody] CreateItemDto itemDto)
      {
         var item = new ReadItemDto(
            Guid.NewGuid(),
            itemDto.Name,
            itemDto.Description,
            itemDto.Price,
            DateTimeOffset.UtcNow
         );
         items.Add(item);
         return CreatedAtAction(nameof(GetItemById), new {Id = item.Id}, item);
      }
   }
}