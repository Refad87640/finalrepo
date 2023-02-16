using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class PermissionUser
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }

         }
}
