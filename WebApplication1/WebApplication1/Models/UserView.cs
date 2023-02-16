using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class UserView
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int UserStatus { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = null!;
    }
}
