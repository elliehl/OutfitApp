using System;
namespace OutfitTracker.DTOs
{
	public class ItemDTO
	{
        public string? Name { get; set; }
        public string Item_Type { get; set; }
        public double? Price { get; set; }
        public DateTime? Date_Bought { get; set; }
        public string? Primary_Colour { get; set; }
        public string? Secondary_Colour { get; set; }
        public string? Brand { get; set; }
        public string? Image_Path { get; set; }
        public bool Is_Available_To_Wear { get; set; }
        public int Quantity { get; set; }
    }
}

