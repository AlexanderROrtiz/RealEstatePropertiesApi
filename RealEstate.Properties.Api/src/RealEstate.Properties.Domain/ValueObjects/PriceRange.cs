
namespace RealEstate.Properties.Domain.ValueObjects
{
    public class PriceRange
    {
        public decimal Min { get; }
        public decimal Max { get; }

        public PriceRange(decimal min, decimal max)
        {
            if (min < 0 || max < 0 || min > max)
                throw new ArgumentException("Invalid price range");
            Min = min;
            Max = max;
        }
    }
}
