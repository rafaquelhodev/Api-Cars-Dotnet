using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_Cars_Dotnet.Models
{
    public class Car : EntityBase
    {
        public string Color { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Age { get; set; }

        public bool IsValid()
        {
            bool valid = true;

            if (Price < 0 || Age < 0)
                valid = false;

            return valid;
        }
    }
}