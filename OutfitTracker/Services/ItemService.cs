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

		public async Task<IEnumerable<GetItemDTO>> GetClothingItems()
		{
			var repositoryResponse = await _itemRepository.GetClothingItems();
			var mappedValue = repositoryResponse.Select(_mapper.Map<GetItemDTO>);
			return mappedValue;
		}

        public async Task<GetItemDTO> GetClothingItemById(int itemId)
        {
            var repositoryResponse = await _itemRepository.GetClothingItemById(itemId);
            var mappedValue = _mapper.Map<GetItemDTO>(repositoryResponse);
            return mappedValue;
        }

        public async Task<int> AddClothingItem(ItemDTO item)
        {
			var primaryColourId = await _itemRepository.GetColourId(item.Primary_Colour);
            var secondaryColourId = await _itemRepository.GetColourId(item.Secondary_Colour);
			var typeId = await _itemRepository.GetTypeId(item.Item_Type);

            var mappedValue = _mapper.Map<AddItemEntity>(item);
			mappedValue.Primary_Colour_Id = primaryColourId;
            mappedValue.Secondary_Colour_Id = secondaryColourId;
			mappedValue.Item_Type_Id = typeId;

            return await _itemRepository.AddClothingItem(mappedValue);
        }

		public async Task DeleteItem (int itemId)
		{
			await _itemRepository.DeleteItem(itemId);
		}

		public async Task UpdateClothingItem(ItemDTO item, int itemId)
		{
            var primaryColourId = await _itemRepository.GetColourId(item.Primary_Colour);
            var secondaryColourId = await _itemRepository.GetColourId(item.Secondary_Colour);
            var typeId = await _itemRepository.GetTypeId(item.Item_Type);

            var mappedValue = _mapper.Map<AddItemEntity>(item);
            mappedValue.Primary_Colour_Id = primaryColourId;
            mappedValue.Secondary_Colour_Id = secondaryColourId;
            mappedValue.Item_Type_Id = typeId;

			await _itemRepository.UpdateClothingItem(mappedValue, itemId);
        }
    }

	public interface IItemService
	{
		Task<IEnumerable<GetItemDTO>> GetClothingItems();
		Task<GetItemDTO> GetClothingItemById(int itemId);
		Task<int> AddClothingItem(ItemDTO item);
		Task DeleteItem(int itemId);
		Task UpdateClothingItem(ItemDTO item, int itemId);
    }
}

