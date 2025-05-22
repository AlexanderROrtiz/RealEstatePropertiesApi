
using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.Properties
{
    public class GetPropertiesQuery : IRequest<IEnumerable<PropertyDto>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public GetPropertiesQuery(string name = null, string address = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            Name = name;
            Address = address;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
