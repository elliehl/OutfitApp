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
			var query =		"SELECT i.id, i.name, type.name AS item_type, price, c1.name AS " +
							"primary_colour, c2.name AS secondary_colour, date_bought, brand, image_path, " + "is_available_to_wear, quantity " +
							"FROM item i " +
							"LEFT OUTER JOIN colour c1 ON c1.id = i.primary_colour_id " +
							"LEFT OUTER JOIN colour c2 on c2.id = i.secondary_colour_id " +
							"JOIN type ON type.id = i.type_id";

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
							"first_colour, c2.name AS second_colour, brand, is_available_to_wear, quantity " +
                            "FROM item i " +
                            "LEFT OUTER JOIN colour c1 ON c1.id = i.primary_colour_id " +
                            "LEFT OUTER JOIN colour c2 on c2.id = i.secondary_colour_id " +
                            "JOIN type ON type.id = i.type_id " +
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

        public async Task<int> AddClothingItem(AddItemEntity item)
        {
			var parameters = new { Name = item.Name, Item_Type = item.Item_Type_Id, Price = item.Price, Date_Bought =
								   item.Date_Bought, Primary_Colour = item.Primary_Colour_Id, Secondary_Colour =
								   item.Secondary_Colour_Id, Brand = item.Brand, Image_Path = item.Image_Path, Is_Available_To_Wear = item.Is_Available_To_Wear, Quantity = item.Quantity};
			var query = "INSERT INTO item (name, type_id, price, date_bought, " +
						"primary_colour_id, secondary_colour_id, " +
                        "brand, image_path, is_available_to_wear, quantity) " +
						"VALUES (@Name, @Item_Type, @Price, @Date_Bought, @Primary_Colour, @Secondary_Colour, " +
						"@Brand, @Image_Path, @Is_Available_To_Wear, @Quantity); " +
						"SELECT LAST_INSERT_ID()";

            try
            {
                using var connection = _context.GetConnection();
                return await connection.ExecuteScalarAsync<int>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteItem(int itemId)
		{
			var parameters = new { Id = itemId };
			var query = "DELETE FROM item WHERE id = @Id";

			try
			{
				using var connection = _context.GetConnection();
				await connection.ExecuteAsync(query, parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		public async Task UpdateClothingItem(AddItemEntity item, int itemId)
		{
			var parameters = new
			{
				Name = item.Name,
				Item_Type = item.Item_Type_Id,
				Price = item.Price,
				Date_Bought = item.Date_Bought,
				Primary_Colour = item.Primary_Colour_Id,
				Secondary_Colour = item.Secondary_Colour_Id,
				Brand = item.Brand,
				Image_Path = item.Image_Path,
				Is_Available_To_Wear = item.Is_Available_To_Wear,
				Quantity = item.Quantity,
				Id = itemId
			};
			var query = "UPDATE item " +
						"SET name = @Name, type_id = @Item_Type, price = Price, date_bought = @Date_Bought, " +
						"primary_colour_id = @Primary_Colour, secondary_colour_id = @Secondary_Colour, " +
						"brand = @Brand, image_path = @Image_Path, is_available_to_wear = @Is_Available_To_Wear, " +
						"quantity = @Quantity " +
						"WHERE id = @Id";

			try
			{
				using var connection = _context.GetConnection();
				await connection.ExecuteAsync(query, parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		public async Task<int> GetColourId(string colour)
		{
			var parameters = new { Colour = colour };
			var query = "SELECT id FROM colour WHERE name = @Colour";

			try
			{
				using var connection = _context.GetConnection();
				int id = await connection.QuerySingleAsync<int>(query, parameters);
				return id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

        public async Task<int> GetTypeId(string type)
        {
            var parameters = new { Type = type };
            var query = "SELECT id FROM type WHERE name = @Type";

            try
            {
                using var connection = _context.GetConnection();
                int id = await connection.QuerySingleAsync<int>(query, parameters);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }

	public interface IItemRepository
	{
		Task<IEnumerable<ItemEntity>> GetClothingItems();
		Task<ItemEntity> GetClothingItemById(int itemId);
		Task<int> AddClothingItem(AddItemEntity item);
		Task DeleteItem(int itemId);
        Task<int> GetColourId(string colour);
		Task<int> GetTypeId(string type);
		Task UpdateClothingItem(AddItemEntity item, int itemId);
    }
}

