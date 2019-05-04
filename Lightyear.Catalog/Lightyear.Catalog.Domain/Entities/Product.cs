using Lightyear.Catalog.Domain.Abstractions;

namespace Lightyear.Catalog.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
