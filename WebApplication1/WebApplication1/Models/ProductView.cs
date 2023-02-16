using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ProductView
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public byte[]? Image { get; set; }
        public string? Note { get; set; }
        public string ProductTypeName { get; set; } = null!;
    }
}
