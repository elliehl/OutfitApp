﻿using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using OutfitTracker.DTOs;
using OutfitTracker.Entities;
using OutfitTracker.Services;
using OutfitTracker.Responses;

namespace OutfitTracker.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]

    public class ItemController: ControllerBase
	{
		private readonly IItemService _itemService;
		private readonly IMapper _mapper;

		public ItemController(IItemService itemService, IMapper mapper)
		{
            _itemService = itemService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetClothingItems()
		{
			var clothingItems = await _itemService.GetClothingItems();
			return Ok(clothingItems);
		}

		[HttpGet("{itemId}")]
		public async Task<IActionResult> GetClothingItemById([FromRoute] int itemId)
		{
			var clothingItem = await _itemService.GetClothingItemById(itemId);
			return Ok(clothingItem);
		}

        [HttpPost]
        public async Task<IActionResult> AddClothingItem([FromBody] ItemDTO item)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            var createdClothingItemId = await _itemService.AddClothingItem(item);
			var createdClothingItem = _mapper.Map<ItemResponse>(item);
			createdClothingItem.Id = createdClothingItemId;
            return CreatedAtAction("AddClothingItem", createdClothingItem);
        }

		[HttpDelete("{itemId}")]
		public async Task<IActionResult> DeleteItem([FromRoute] int itemId)
		{
			await _itemService.DeleteItem(itemId);
			return Ok("Item successfully deleted");
		}

		[HttpPut("{itemId}")]
		public async Task<IActionResult> UpdateClothingItem([FromBody] ItemDTO item, [FromRoute] int itemId)
		{
			await _itemService.UpdateClothingItem(item, itemId);
			return Ok();
		}
    }
}

