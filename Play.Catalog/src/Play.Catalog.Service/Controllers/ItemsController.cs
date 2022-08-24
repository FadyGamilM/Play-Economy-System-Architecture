using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Interfaces;
using Play.Catalog.Service.Models;
using Play.Catalog.Service.Repositories;
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
      private readonly IItemsRepo _itemsRepo;
      public ItemsController(IItemsRepo itemsRepo)
      {
         this._itemsRepo = itemsRepo;
      }

      // --> URL: api/items
      // --> Desc: Get all items from DB
      // --> response: status code 200 and list of items
      [HttpGet("")]
      public async Task<IActionResult> GetItems ()
      {
         var items = (await this._itemsRepo.GetItems()).Select(item => item.AsDto());
         return Ok(items);
      }

      // --> URL: api/items/{Id}
      // --> Desc: Get one item by its id
      // --> response: status code 200 and returns an item
      [HttpGet("{Id}", Name="GetItemsById")]
      public async Task<IActionResult> GetItemById([FromRoute]Guid Id)
      {
         var item = await this._itemsRepo.GetItemById(Id);
         if (item == null){
            return NotFound();
         }
         var foundItem = item.AsDto();
         return Ok(item);
      }

      // --> URL: api/items
      // --> Desc: create a new item
      // --> response: status code 201 and returns the created item
      [HttpPost]
      public async Task<IActionResult> CreateItem([FromBody] CreateItemDto itemDto)
      {
         var item = new Item{
            Name = itemDto.Name,
            Description = itemDto.Description,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
         };
         await this._itemsRepo.CreateItem(item);
         return CreatedAtAction(nameof(GetItemById), new {Id = item.Id}, item);
      }

      // --> URL: api/items/{Id}
      // --> Desc: Update an existing Item given the Id and the complete object you want to update 
      // --> response: No Content with 204 status code
      [HttpPut("{Id}")]
      public async Task<IActionResult> UpdateItem([FromRoute] Guid Id, [FromBody] CreateItemDto itemDto)
      {
         var existingItem = await this._itemsRepo.GetItemById(Id);
         if (existingItem == null){
            return NotFound();
         } 
         var item = existingItem with
         {
            Name = itemDto.Name,
            Description = itemDto.Description,
            Price = itemDto.Price
         };
         await this._itemsRepo.UpdateItem(Id, item);
         return NoContent();
      }

      // --> URL: api/items{Id}
      // --> Desc: Delete an existing item given its Id
      // --> response:  204 No content
      [HttpDelete("{Id}")]
      public async Task<IActionResult> DeleteItem([FromRoute] Guid Id)
      {
         var item = await this._itemsRepo.GetItemById(Id);
         if (item == null){
            return NotFound();
         }
         await this._itemsRepo.DeleteItem(Id);
         return NoContent();
      }
}
}














