using System;

namespace Products.Domain
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; } 
    }
}
