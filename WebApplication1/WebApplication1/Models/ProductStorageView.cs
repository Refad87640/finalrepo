using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ProductStorageView
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; } = null!;
        public byte[]? ProductImage { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } = null!;
        public string StoreName { get; set; } = null!;
    }
}
