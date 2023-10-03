using System;
namespace OutfitTracker.DTOs
{
	public class ItemDTO
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Item_Type { get; set; }
        public double? Price { get; set; }
        public DateTime? Date_Bought { get; set; }
        public string? Primary_Colours { get; set; }
        public string? Secondary_Colours { get; set; }
        public string? Brand { get; set; }
        public string? Image_Path { get; set; }
    }
}

