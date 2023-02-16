﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class InvoiceProduct
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long InvoiceId { get; set; }
        public int StoreId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string? Note { get; set; }

      
    }
}
