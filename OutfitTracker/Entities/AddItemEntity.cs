using System;
namespace OutfitTracker.Entities
{
	public class AddItemEntity
	{
        public string? Name { get; set; }
        public int Item_Type_Id { get; set; }
        public double? Price { get; set; }
        public DateTime? Date_Bought { get; set; }
        public int? Primary_Colour_Id { get; set; }
        public int? Secondary_Colour_Id { get; set; }
        public string? Brand { get; set; }
        public string? Image_Path { get; set; }
        public bool Is_Available_To_Wear { get; set; }
        public int Quantity { get; set; }
    }
}

