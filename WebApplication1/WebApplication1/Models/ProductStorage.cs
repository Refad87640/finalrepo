using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ProductStorage
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }

         }
}
