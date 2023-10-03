using AutoMapper;
using OutfitTracker.DTOs;
using OutfitTracker.Entities;

namespace OutfitTracker.Mappers
{
	public class AutoMapper: Profile
	{
		public AutoMapper()
		{
			CreateMap<ItemEntity, ItemDTO>();
            CreateMap<ItemDTO, ItemEntity>();
        }
    }
}

