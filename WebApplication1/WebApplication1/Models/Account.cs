using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Account
    {
        public Account()
        {
            
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public int AccountTypeId { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateAt { get; set; }

        }
}
