using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using static Play.Catalog.Service.Models.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto (Guid.NewGuid(), "Potion", "Resores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto (Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto (Guid.NewGuid(), "Bronze sword", "Deal a small amount of damage", 20, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.FirstOrDefault(x=>x.Id ==id);

            return item != null ? item : NotFound();
        }

        [HttpPost]
       public ActionResult<ItemDto> AddItem(CreateItemDto createItemDto)
       {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
       } 

        [HttpPut("{id}")]
       public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
       {
             var existingItem = items.FirstOrDefault(x=>x.Id ==id);

             if(existingItem == null){
                return NotFound();
             }

             var updatedItem = existingItem with{
                Name = updateItemDto.Name,
                Price = updateItemDto.Price,
                Description = updateItemDto.Description
             };

             var index = items.FindIndex(existingItem=>existingItem.Id == id);
            items[index] = updatedItem;

            return NoContent();
       }
        [HttpDelete("{id}")]
       public IActionResult Delete(Guid id){
            var index = items.FindIndex(existingItem=>existingItem.Id == id);

            if(index <0){
                return NotFound();
            }  

            items.RemoveAt(index);

            return NoContent();
       }
    }
}