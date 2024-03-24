using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository _repository;

    public ItemsController(IItemsRepository itemsRepository)
    {
        _repository = itemsRepository;
    }

    // GET /items 
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = (await _repository.GetItemsAsynch())
                    .Select(item => item.AsDto());
        return items;
    }

    // GET /items/{id} 
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
        var item = await _repository.GetItemAsynch(id);
        if (item is null)
        {
            return NotFound();
        }
        return item.AsDto();
    }

    // POST /items 
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreateDate = DateTimeOffset.UtcNow
        };
        await _repository.CreateItemAsynch(item);

        return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
    }

    // PUT /items/{id} 
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
        var existingItem = await _repository.GetItemAsynch(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        Item updatedItem = existingItem with
        {
            Name = itemDto.Name,
            Price = itemDto.Price
        };

        await _repository.UpdateItemAsynch(updatedItem);

        return NoContent();
    }

    // DELETE /items/{id} 
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletemItemAsync(Guid id)
    {
        var existingItem = await _repository.GetItemAsynch(id);
        if (existingItem is null)
        {
            return NotFound();
        }

        await _repository.DeleteItemAsynch(id);
        return NoContent();
    }
}
