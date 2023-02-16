using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
              }

        public long Id { get; set; }
        public int UserId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int Status { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal Price { get; set; }

          }
}
