using AutoMapper;
using OutfitTracker.DTOs;
using OutfitTracker.Entities;
using OutfitTracker.Responses;

namespace OutfitTracker.Mappers
{
	public class AutoMapper: Profile
	{
		public AutoMapper()
		{
			CreateMap<ItemEntity, ItemDTO>();
            CreateMap<ItemDTO, AddItemEntity>();
            CreateMap<ItemDTO, ItemResponse>();
        }
    }
}

