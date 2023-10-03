using OutfitTracker.DTOs;
using OutfitTracker.Repositories;
using AutoMapper;
using OutfitTracker.Entities;

namespace OutfitTracker.Services
{
	public class ItemService: IItemService
	{
		private readonly IItemRepository _itemRepository;
		private readonly IMapper _mapper;

		public ItemService(IItemRepository itemRepository, IMapper mapper)
		{
			_itemRepository = itemRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ItemDTO>> GetClothingItems()
		{
			var repositoryResponse = await _itemRepository.GetClothingItems();
			var mappedValue = repositoryResponse.Select(_mapper.Map<ItemDTO>);
			return mappedValue;
		}

        public async Task<ItemDTO> GetClothingItemById(int itemId)
        {
            var repositoryResponse = await _itemRepository.GetClothingItemById(itemId);
            var mappedValue = _mapper.Map<ItemDTO>(repositoryResponse);
            return mappedValue;
        }
    }

	public interface IItemService
	{
		Task<IEnumerable<ItemDTO>> GetClothingItems();
		Task<ItemDTO> GetClothingItemById(int itemId);
    }
}

