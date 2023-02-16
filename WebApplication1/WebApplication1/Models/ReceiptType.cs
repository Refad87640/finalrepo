using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ReceiptType
    {
        public ReceiptType()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Status { get; set; }
        public string? Note { get; set; }

          }
}
