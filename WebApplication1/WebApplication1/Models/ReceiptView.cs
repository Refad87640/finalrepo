using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ReceiptView
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ReceiptTypeId { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreateAt { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public string ReceiptTypeName { get; set; } = null!;
        public string AccountFromName { get; set; } = null!;
        public string AccountToName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = null!;
        public int UserStatus { get; set; }
    }
}
