using System;

namespace Play.Catalog.Service.Repository.Models
{
    public class Item
    {
        public Guid IdMyProperty { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}