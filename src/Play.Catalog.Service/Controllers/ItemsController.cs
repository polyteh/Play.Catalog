using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Extensions;
using Play.Catalog.Service.Repository;
using Play.Catalog.Service.Repository.Models;
using static Play.Catalog.Service.Models.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _itemsRepository.GetAllAsync()).Select(x=>x.AsDto());

            return items;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = (await _itemsRepository.GetAsync(id)).AsDto();

            return item != null ? item : NotFound();
        }

        [HttpPost]
       public async Task<ActionResult<ItemDto>> AddItemAsync(CreateItemDto createItemDto)
       {
            var item = new Item()
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate =DateTimeOffset.UtcNow
            };

            await _itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
       } 

        [HttpPut("{id}")]
       public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
       {
             var existingItem = await _itemsRepository.GetAsync(id);

             if(existingItem == null){
                return NotFound();
             }
            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _itemsRepository.UpdateAsync(existingItem);

            return NoContent();
       }
        [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteAsync(Guid id){
            var existingItem = await _itemsRepository.GetAsync(id);

            if(existingItem == null){
                return NotFound();
            }  

            await _itemsRepository.RemoveAsync(id);

            return NoContent();
       }
    }
}