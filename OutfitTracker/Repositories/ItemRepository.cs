using System;
using Dapper;
using OutfitTracker.Data;
using OutfitTracker.Entities;

namespace OutfitTracker.Repositories
{
	public class ItemRepository: IItemRepository
	{
		private readonly IContext _context;

		public ItemRepository(IContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ItemEntity>> GetClothingItems()
		{
			IEnumerable<ItemEntity> clothingItems = new List<ItemEntity>();
			var query =		"SELECT i.id, i.name, type.name AS item_type, price, c1.name AS " +										  "primary_colours, c2.name AS secondary_colours, brand " +
							"FROM item i " +
							"LEFT OUTER JOIN colour c1 ON c1.id = i.primary_colour " +
							"LEFT OUTER JOIN colour c2 on c2.id = i.secondary_colour " +
							"JOIN type ON type.id = i.type";

			try
			{
				using var connection = _context.GetConnection();
				clothingItems = await connection.QueryAsync<ItemEntity>(query);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
			return clothingItems;
		}

		public async Task<ItemEntity> GetClothingItemById(int itemId)
		{
            ItemEntity clothingItem = new ItemEntity();
			var parameters = new { Id = itemId };
			var query =		"SELECT i.id, i.name, type.name AS item_type, price, c1.name AS " +
							"primary_colours, c2.name AS secondary_colours, brand " +
                            "FROM item i " +
                            "LEFT OUTER JOIN colour c1 ON c1.id = i.primary_colour " +
                            "LEFT OUTER JOIN colour c2 on c2.id = i.secondary_colour " +
                            "JOIN type ON type.id = i.type " +
							"WHERE i.id = @Id";

			try
			{
				using var connection = _context.GetConnection();
				clothingItem = await connection.QueryFirstAsync<ItemEntity>(query, parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
			return clothingItem;
        }

    }

	public interface IItemRepository
	{
		Task<IEnumerable<ItemEntity>> GetClothingItems();
		Task<ItemEntity> GetClothingItemById(int itemId);
    }
}

