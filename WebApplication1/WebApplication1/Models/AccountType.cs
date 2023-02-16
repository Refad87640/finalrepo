using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Status { get; set; }
        public string? Note { get; set; }
        public int UserId { get; set; }

       }
}
