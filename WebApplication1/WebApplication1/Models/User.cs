using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class User
    {
        public User()
        {
              }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserTypeId { get; set; }
        public DateTime CreateAt { get; set; }
        public int Status { get; set; }
        public string Password { get; set; } = null!;
        public string Ename { get; set; } = null!;

         }
}
