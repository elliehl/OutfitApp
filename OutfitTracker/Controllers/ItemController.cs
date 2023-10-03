using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using OutfitTracker.DTOs;
using OutfitTracker.Entities;
using OutfitTracker.Services;

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
	}
}

