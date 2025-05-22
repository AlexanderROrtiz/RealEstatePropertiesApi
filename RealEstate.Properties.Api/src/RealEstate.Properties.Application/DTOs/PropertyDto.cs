
namespace RealEstate.Properties.Application.DTOs
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string ImageUrl { get; set; }
    }
}
